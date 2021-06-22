using System.Collections.Generic;
using System.Collections.ObjectModel;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Data
{
    public class Permissions
    {
        private static readonly List<Permission> _permissions = new List<Permission>();

        public static readonly Permission CreateProject = CreatePermission(1, "Create Project", "Permission to create new projects.", PermissionType.Global);
        public static readonly Permission DeleteProject = CreatePermission(2, "Delete Project", "Permission to delete existing projects.", PermissionType.Global);

        private static Permission CreatePermission(int id, string name, string description, PermissionType type)
        {
            var permission = new Permission { Id = id, Name = name, Description = description, Type = type };
            _permissions.Add(permission);
            return permission;
        }

        public static ReadOnlyCollection<Permission> GetAll()
        {
            return _permissions.AsReadOnly();
        }
    }
}