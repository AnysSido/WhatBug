using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Data
{
    public class Permissions
    {
        private static readonly List<Permission> _permissions = new List<Permission>();

        public static readonly Permission CreateProject = CreatePermission(1, "Create Project", "Create new projects.", PermissionType.Global);
        public static readonly Permission DeleteProject = CreatePermission(2, "Delete Project", "Delete existing projects.", PermissionType.Global);
        public static readonly Permission EditUserPermissions = CreatePermission(3, "Edit User Permissions", "Edit global permissions assigned to users.", PermissionType.Global);
        public static readonly Permission ViewAllProjects = CreatePermission(4, "View All Projects", "View all projects in WhatBug. Users without this permission must be a member of a project to view it.", PermissionType.Global);

        private static Permission CreatePermission(int id, string name, string description, PermissionType type)
        {
            var permission = new Permission { Id = id, Name = name, Description = description, Type = type };
            _permissions.Add(permission);
            return permission;
        }

        public static ReadOnlyCollection<Permission> Seed()
        {
            return _permissions.AsReadOnly();
        }

        public static ReadOnlyCollection<Permission> GetAll(PermissionType type)
        {
            return _permissions.Where(p => p.Type == type).ToList().AsReadOnly();
        }
    }
}