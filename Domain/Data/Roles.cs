using System.Collections.Generic;
using System.Collections.ObjectModel;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Data
{
    public class Roles
    {
        private static readonly List<Role> _roles = new List<Role>();

        public static readonly Role Administrator = CreateRole(1, "Administrator");
        public static readonly Role User = CreateRole(2, "User");

        private static Role CreateRole(int id, string name)
        {
            var role = new Role { Id = id, Name = name };
            _roles.Add(role);
            return role;
        }

        public static ReadOnlyCollection<Role> Seed()
        {
            return _roles.AsReadOnly();
        }
    }
}