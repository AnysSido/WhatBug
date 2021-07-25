using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Data
{
    public class Permissions
    {
        private static readonly List<Permission> _permissions = new List<Permission>();

        // Global
        public const string CreateProject = "Create Project";
        public const string DeleteProject = "Delete Project";
        public const string EditUserPermissions = "Edit User Permissions";
        public const string ViewAllProjects = "View All Projects";

        // Project
        public const string CreateIssue = "Create Issue";
        public const string EditIssue = "Edit Issue";
        public const string DeleteIssue = "Delete Issue";

        static Permissions()
        {
            CreatePermission(1, CreateProject, "Create new projects.", PermissionType.Global);
            CreatePermission(2, DeleteProject, "Delete existing projects.", PermissionType.Global);
            CreatePermission(3, EditUserPermissions, "Edit global permissions assigned to users.", PermissionType.Global);
            CreatePermission(4, ViewAllProjects, "View all projects in WhatBug. Users without this permission must be a member of a project to view it.", PermissionType.Global);

            CreatePermission(5, CreateIssue, "Create new issues within a project.", PermissionType.Project);
            CreatePermission(6, EditIssue, "Edit existing issues within a project.", PermissionType.Project);
            CreatePermission(7, DeleteIssue, "Delete issues within a project.", PermissionType.Project);
        }

        private static Permission CreatePermission(int id, string name, string description, PermissionType type)
        {
            var permission = new Permission { Id = id, Name = name, Description = description, Type = type };
            _permissions.Add(permission);
            return permission;
        }

        public static Permission ToEntity(string permission)
        {
            return _permissions.First(p => p.Name == permission);
        }

        public static ReadOnlyCollection<Permission> GetAll(PermissionType type)
        {
            return _permissions.Where(p => p.Type == type).ToList().AsReadOnly();
        }

        public static ReadOnlyCollection<Permission> Seed()
        {
            return _permissions.AsReadOnly();
        }
    }
}