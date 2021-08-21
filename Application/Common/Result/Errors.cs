namespace WhatBug.Application.Common.Result
{
    public static class Errors
    {
        public static class PermissionScheme
        {
            public static Error NameIsTaken(string schemeName) =>
                new Error("SchemeNameIsTaken", $"A permission scheme with the name {schemeName} already exists");
        }
    }

}
