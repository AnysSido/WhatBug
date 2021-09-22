using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Authorization
{
    /* 
        Permission extensions to be used in .cshtml files to show/hide elements based on permissions.
     */
    public static class PermissionExtensions
    {
        public static bool HasPermission(this ClaimsPrincipal user, string permission)
        {
            if (user == null)
                return false;

            return user?.Claims.Any(c => c.Value == permission) ?? default;
        }

        public static bool HasPermissions(this ClaimsPrincipal user, params string[] permissions)
        {
            if (user == null)
                return false;

            foreach (string permission in permissions)
            {
                if (!user.Claims.Any(c => c.Value == permission))
                    return false;
            }
            return true;
        }

        public static bool HasAnyPermission(this ClaimsPrincipal user, params string[] permissions)
        {
            if (user == null)
                return false;

            foreach (string permission in permissions)
            {
                if (user.Claims.Any(c => c.Value == permission))
                    return true;
            }
            return false;
        }
    }
}
