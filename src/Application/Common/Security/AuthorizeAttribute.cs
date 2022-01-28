using System;

namespace WhatBug.Application.Common.Security
{
    public enum PermissionOperator
    {
        And = 1,
        Or = 2
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Attribute
    {
        public PermissionOperator Operator;
        public string[] Permissions;

        public AuthorizeAttribute(params string[] permissions)
        {
            Operator = PermissionOperator.And;
            Permissions = permissions;
        }

        public AuthorizeAttribute(PermissionOperator permissionOperator, params string[] permissions)
        {
            Operator = permissionOperator;
            Permissions = permissions;
        }
    }
}
