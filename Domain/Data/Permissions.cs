using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Data
{
    public class Permissions
    {
        public enum Global { CreateProject, DeleteProject }

        public static readonly ReadOnlyDictionary<Global, Permission> GlobalPermissions;

        static Permissions()
        {
            var globalPermissions = new Dictionary<Global, Permission>()
            {
                {  Global.CreateProject, new Permission(1, "Create Project", "Permission to create new projects.", PermissionType.Global) },
                {  Global.DeleteProject, new Permission(2, "Delete Project", "Permission to delete existing projects.", PermissionType.Global) }
            };

            GlobalPermissions = new ReadOnlyDictionary<Global, Permission>(globalPermissions);
        }
    }
}