using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Domain.Entities.JoinTables
{
    public class PermissionSchemeProjectRolePermission
    {
        public int PermissionSchemeId { get; set; }
        public PermissionScheme PermissionScheme { get; set; }

        public int ProjectRoleId { get; set; }
        public ProjectRole ProjectRole { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
