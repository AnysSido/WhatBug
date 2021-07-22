using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Authorization
{
    public enum PermissionOperator
    {
        And = 1,
        Or = 2
    }

    /*
    * This attribute builds a unique name using the attribute paramaters.
    * Policies are cached by the framework and so must always return the same policy
    * for a given name. This class will always return the same name for the same set
    * of required permissions and/or operator.
    */

    public class RequirePermissionAttribute : AuthorizeAttribute
    {
        internal const string PolicyPrefix = "PERMISSION_";
        private const string Separator = "_";

        public RequirePermissionAttribute(PermissionOperator permissionOperator, params string[] permissions)
        {
            // E.g: PERMISSION_2_PermissionA_PermissionB
            Policy = $"{PolicyPrefix}{(int)permissionOperator}{Separator}{string.Join(Separator, permissions)}";
        }

        public RequirePermissionAttribute(string permission)
        {
            // E.g: PERMISSION_1_PermissionA
            Policy = $"{PolicyPrefix}{(int)PermissionOperator.And}{Separator}{permission}";
        }

        public static PermissionOperator GetOperatorFromPolicy(string policyName)
        {
            var permissionOperator = int.Parse(policyName.AsSpan(PolicyPrefix.Length, 1));
            return (PermissionOperator)permissionOperator;
        }

        public static string[] GetPermissionsFromPolicy(string policyName)
        {
            return policyName.Substring(PolicyPrefix.Length + 2)
                .Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
