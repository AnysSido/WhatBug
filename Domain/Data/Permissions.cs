using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WhatBug.Domain.Entities;

namespace WhatBug.Domain.Data
{
    public class Permissions
    {
        private static readonly List<Permission> _permissions = new List<Permission>();

        // Global
        public const string CreateProject = "Create Project";
        public const string DeleteProject = "Delete Project";
        public const string ManageUserPermissions = "Manage User Permissions";
        public const string ViewAllProjects = "View All Projects";
        public const string ManageProjectRoles = "Manage Project Roles";
        public const string ManagePermissionSchemes = "Manage Permission Schemes";
        public const string ManagePriorities = "Manage Priorities";
        public const string ManagePrioritySchemes = "Manage Priority Schemes";

        // Project
        public const string CreateIssue = "Create Issue";
        public const string EditIssue = "Edit Issue";
        public const string DeleteIssue = "Delete Issue";

        static Permissions()
        {
            CreatePermission(1, CreateProject, "Create new projects.", PermissionType.Global);
            CreatePermission(2, DeleteProject, "Delete existing projects.", PermissionType.Global);
            CreatePermission(3, ManageUserPermissions, "Grant and/or deny user-level permissions.", PermissionType.Global);
            CreatePermission(4, ViewAllProjects, "View all projects in WhatBug. Users without this permission must be a member of a project to view it.", PermissionType.Global);
            CreatePermission(5, ManageProjectRoles, "Create, edit and delete project roles used by permission schemes.", PermissionType.Global);
            CreatePermission(9, ManagePermissionSchemes, "Create, edit and delete permission schemes.", PermissionType.Global);
            CreatePermission(10, ManagePriorities, "Create, edit and delete priorities.", PermissionType.Global);
            CreatePermission(11, ManagePrioritySchemes, "Create, edit and delete priority schemes.", PermissionType.Global);

            CreatePermission(6, CreateIssue, "Create new issues within a project.", PermissionType.Project);
            CreatePermission(7, EditIssue, "Edit existing issues within a project.", PermissionType.Project);
            CreatePermission(8, DeleteIssue, "Delete issues within a project.", PermissionType.Project);
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