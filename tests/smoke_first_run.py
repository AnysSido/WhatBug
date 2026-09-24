import concurrent.futures
import http.cookiejar
import html
import json
from pathlib import Path
import re
import subprocess
import tempfile
import threading
import time
import urllib.error
import urllib.parse
import urllib.request
import uuid

ROOT = Path(__file__).resolve().parents[1]
PROJECT = "whatbug-smoke-" + uuid.uuid4().hex[:10]
PASSWORD = "Smoke-test-password-42"


def run(*args, data=None):
    return subprocess.run(args, input=data, text=True, capture_output=True, check=True, cwd=ROOT).stdout


class Client:
    def __init__(self, base):
        self.base = base
        self.opener = urllib.request.build_opener(urllib.request.HTTPCookieProcessor(http.cookiejar.CookieJar()))

    def request(self, path, fields=None):
        data = urllib.parse.urlencode(fields).encode() if fields is not None else None
        try:
            response = self.opener.open(self.base + path, data=data, timeout=30)
        except urllib.error.HTTPError as error:
            response = error
        return response.status, response.read().decode(), response.url

    def form(self, path):
        status, body, _ = self.request(path)
        assert status == 200, (path, status)
        token = re.search(r'name="__RequestVerificationToken"[^>]*value="([^"]+)"', body)
        assert token, path
        return html.unescape(token.group(1)), body

    def register(self, name, token):
        return self.request("/register", {
            "Username": name, "Email": name.replace(" ", "") + "@example.test",
            "Password": PASSWORD, "ConfirmPassword": PASSWORD, "AgreeToTerms": "true",
            "__RequestVerificationToken": token,
        })

    def login(self, name):
        token, _ = self.form("/login")
        status, body, url = self.request("/login", {
            "Username": name, "Password": PASSWORD, "__RequestVerificationToken": token,
        })
        assert status == 200 and "/login" not in url, ("login", name, status, url)
        return body


with tempfile.TemporaryDirectory(prefix=PROJECT) as temp:
    override = Path(temp) / "compose.yml"
    override.write_text("""services:
  webui:
    ports: !override
      - "127.0.0.1::80"
  postgres:
    ports: !reset []
""")
    compose = ["docker", "compose", "-p", PROJECT, "-f", str(ROOT / "docker-compose.yml"), "-f", str(override)]

    def sql(query, database="WhatBug"):
        return run(*compose, "exec", "-T", "postgres", "psql", "-v", "ON_ERROR_STOP=1", "-U", "postgres", "-d", database, "-At", data=query).strip()

    try:
        run(*compose, "up", "-d", "--no-build", "--wait", "--wait-timeout", "120")
        port = run(*compose, "port", "webui", "80").strip()
        base = "http://" + port
        for attempt in range(60):
            try:
                if Client(base).request("/login")[0] == 200:
                    break
            except (OSError, urllib.error.URLError):
                pass
            time.sleep(1)
        else:
            raise AssertionError("App did not start on a fresh database")
        client = Client(base)
        _, login, _ = client.request("/login")
        assert "Create an account" in login and "Admin (Read Only)" not in login
        token, body = client.form("/register")
        assert "Create your administrator account" in body
        client.register("invalid user!", token)
        assert sql('SELECT COUNT(*) FROM "Users";') == "0"
        assert sql('SELECT COUNT(*) FROM "UserPermissions";') == "0"
        print("PASS: failed identity creation leaves initial-admin setup available", flush=True)

        sql("""CREATE FUNCTION reject_registration_commit() RETURNS trigger LANGUAGE plpgsql AS $$
        BEGIN RAISE EXCEPTION 'injected commit failure'; END; $$;
        CREATE CONSTRAINT TRIGGER reject_registration_commit AFTER INSERT ON "Users"
        DEFERRABLE INITIALLY DEFERRED FOR EACH ROW EXECUTE FUNCTION reject_registration_commit();""")
        token, _ = client.form("/register")
        status, _, _ = client.register("firstcandidate", token)
        assert status == 500
        assert sql('SELECT COUNT(*) FROM "Users";') == "0"
        assert sql('SELECT COUNT(*) FROM "UserPermissions";') == "0"
        assert sql('SELECT COUNT(*) FROM "AspNetUsers";', "WhatBugIdentity") == "0"
        sql('DROP TRIGGER reject_registration_commit ON "Users"; DROP FUNCTION reject_registration_commit();')
        print("PASS: application commit failure removes the newly created identity", flush=True)

        barrier = threading.Barrier(2)

        def register(name):
            session = Client(base)
            token, body = session.form("/register")
            assert "Create your administrator account" in body
            barrier.wait(timeout=30)
            status, _, _ = session.register(name, token)
            assert status == 200, ("register", name, status)
            return name

        with concurrent.futures.ThreadPoolExecutor(max_workers=2) as pool:
            names = list(pool.map(register, ["firstcandidate", "secondcandidate"]))
        counts = json.loads(sql("""SELECT json_agg(x) FROM (
            SELECT u."Username" AS name, COUNT(p."PermissionId") AS permissions
            FROM "Users" u LEFT JOIN "UserPermissions" p ON p."UserId" = u."Id"
            GROUP BY u."Username") x;"""))
        global_count = int(sql("""SELECT COUNT(*) FROM "Permissions" WHERE "Type" = 'Global';"""))
        assert sorted(x["permissions"] for x in counts) == [0, global_count], counts
        admin = next(x["name"] for x in counts if x["permissions"])
        ordinary = next(x["name"] for x in counts if not x["permissions"])
        assert "Create your administrator account" not in Client(base).form("/register")[1]
        print("PASS: simultaneous registrations produce exactly one administrator", flush=True)

        duplicate = Client(base)
        token, _ = duplicate.form("/register")
        assert duplicate.register(admin, token)[0] == 500
        assert sql('SELECT COUNT(*) FROM "AspNetUsers";', "WhatBugIdentity") == "2"
        assert sql('SELECT COUNT(*) FROM "Users";') == "2"
        print("PASS: rejected duplicate registration preserves existing accounts", flush=True)

        session = Client(base)
        token, _ = session.form("/register")
        session.register("thirduser", token)
        assert sql('SELECT COUNT(DISTINCT "UserId") FROM "UserPermissions";') == "1"
        session.login(admin)
        token, _ = session.form("/projects/create-project")
        status, _, _ = session.request("/projects/create-project", {
            "Name": "Fresh setup", "Description": "Smoke test", "Key": "FRESH",
            "PrioritySchemeId": "1", "PermissionSchemeId": "1",
            "__RequestVerificationToken": token,
        })
        assert status == 200 and sql('SELECT COUNT(*) FROM "Projects";') == "1"
        print("PASS: administrator can sign in and create a project", flush=True)
        ordinary_session = Client(base)
        ordinary_session.login(ordinary)
        assert ordinary_session.request("/projects/create-project")[0] == 403
        print("PASS: ordinary account cannot create projects", flush=True)
        run(*compose, "restart", "webui")
        assert sql('SELECT COUNT(DISTINCT "UserId") FROM "UserPermissions";') == "1"
        print("PASS: admin assignment persists across app restart", flush=True)
        override.write_text(override.read_text().replace(
            "  webui:\n",
            '  webui:\n    environment:\n      WhatBug__Accounts__RegistrationEnabled: "false"\n'))
        run(*compose, "up", "-d", "--no-build", "webui")
        base = "http://" + run(*compose, "port", "webui", "80").strip()
        for attempt in range(60):
            try:
                status, body, url = Client(base).request("/register")
                if status == 200:
                    break
            except (OSError, urllib.error.URLError):
                pass
            time.sleep(1)
        else:
            raise AssertionError("App did not restart")
        assert "/login" in url and "Create an account" not in body
        assert sql('SELECT COUNT(*) FROM "Users";') == "3"
        print("PASS: deployment configuration can disable registration", flush=True)
    except Exception:
        print(run(*compose, "logs", "--tail=40", "webui"), flush=True)
        raise
    finally:
        run(*compose, "down", "-v")
