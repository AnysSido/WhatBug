namespace WhatBug.Application.Common.Result
{
    public static class Errors
    {
        public static class Admin
        {
            public static class Roles
            {
                public static Error NameIsTaken(string roleName) =>
                new Error("RoleNameIsTaken", $"A role with the name {roleName} already exists");
            }
        }

        public static class PermissionScheme
        {
            public static Error NameIsTaken(string schemeName) =>
                new Error("SchemeNameIsTaken", $"A permission scheme with the name {schemeName} already exists");
        }
    }

}
