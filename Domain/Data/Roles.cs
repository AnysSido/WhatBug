using System.Collections.Generic;
using System.Collections.ObjectModel;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Data
{
    public class Roles
    {
        public enum Global { Administrator, User }

        public static readonly ReadOnlyDictionary<Global, Role> GlobalRoles;

        static Roles()
        {
            var globalRoles = new Dictionary<Global, Role>()
            {
                { Global.Administrator, new Role(1, "Administrator", RoleType.Global) },
                { Global.User, new Role(2, "User", RoleType.Global) }
            };

            GlobalRoles = new ReadOnlyDictionary<Global, Role>(globalRoles);
        }
    }
}