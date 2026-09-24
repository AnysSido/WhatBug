using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatBug.Persistence.Migrations
{
    public partial class CompleteProjectDefaults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProjectAdministrator",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
            migrationBuilder.Sql(@"DO $$
BEGIN
    IF (SELECT COUNT(*) FROM ""Priorities"") = 1
       AND EXISTS (SELECT 1 FROM ""Priorities"" WHERE ""Id"" = 1 AND ""Name"" = 'Default' AND ""IsDefault"" = TRUE AND ""ColorId"" = 63 AND ""IconId"" = 38 AND ""Order"" = 0
           AND ""Description"" = 'The default priority used by all issues without any other priority assigned.')
       AND (SELECT COUNT(*) FROM ""PrioritySchemes"") = 1
       AND EXISTS (SELECT 1 FROM ""PrioritySchemes"" WHERE ""Id"" = 1 AND ""Name"" = 'Default' AND ""IsDefault"" = TRUE)
       AND NOT EXISTS (SELECT 1 FROM ""PrioritySchemePriorities"" WHERE ""PrioritySchemeId"" <> 1 OR ""PriorityId"" <> 1)
       AND NOT EXISTS (SELECT 1 FROM ""Issues"")
    THEN
        UPDATE ""Priorities"" SET ""Name"" = 'Medium', ""Description"" = 'This issue is of average importance.',
            ""ColorId"" = 44, ""IconId"" = 26, ""Order"" = 4 WHERE ""Id"" = 1;
        INSERT INTO ""Priorities"" (""Id"", ""Name"", ""Description"", ""Order"", ""ColorId"", ""IconId"", ""IsDefault"") VALUES
            (2, 'Critical', 'This task is critical and must be implemented/fixed.', 1, 19, 21, FALSE),
            (3, 'Very High', 'This is an important issue that must be resolved.', 2, 18, 17, FALSE),
            (4, 'High', 'This issue is quite important.', 3, 15, 13, FALSE),
            (5, 'Low', 'Low priority issue.', 5, 33, 14, FALSE),
            (6, 'Very Low', 'Other issues should take priority.', 6, 35, 18, FALSE),
            (7, 'Trivial', 'May or may not be dealt with depending on time.', 7, 30, 6, FALSE);
        INSERT INTO ""PrioritySchemePriorities"" (""PrioritySchemeId"", ""PriorityId"")
            SELECT 1, ""Id"" FROM ""Priorities"" ON CONFLICT DO NOTHING;
        PERFORM setval(pg_get_serial_sequence('""Priorities""', 'Id'), (SELECT MAX(""Id"") FROM ""Priorities""));
    END IF;
END $$;
SELECT setval(pg_get_serial_sequence('""Roles""', 'Id'), GREATEST(COALESCE(MAX(""Id""), 0) + 1, 1), FALSE) FROM ""Roles"";
INSERT INTO ""Roles"" (""Name"", ""Description"", ""IsProjectAdministrator"")
    VALUES ('Project Administrator', 'Full project permissions. Automatically assigned to the project creator.', TRUE);
INSERT INTO ""PermissionSchemeRolePermissions"" (""PermissionSchemeId"", ""RoleId"", ""PermissionId"")
    SELECT s.""Id"", r.""Id"", p.""Id"" FROM ""PermissionSchemes"" s CROSS JOIN ""Roles"" r CROSS JOIN ""Permissions"" p
    WHERE r.""IsProjectAdministrator"" AND p.""Type"" = 'Project';
INSERT INTO ""ProjectRoleUsers"" (""ProjectId"", ""RoleId"", ""UserId"")
    SELECT p.""Id"", r.""Id"", p.""CreatedBy"" FROM ""Projects"" p CROSS JOIN ""Roles"" r
    WHERE r.""IsProjectAdministrator"" AND EXISTS (SELECT 1 FROM ""Users"" u WHERE u.""Id"" = p.""CreatedBy"")
    AND NOT EXISTS (SELECT 1 FROM ""ProjectRoleUsers"" existing WHERE existing.""ProjectId"" = p.""Id"");
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new System.NotSupportedException("This data migration cannot be reversed safely. Restore a database backup to revert it.");
        }
    }
}
