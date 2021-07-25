using System.Collections.Generic;
using WhatBug.Domain.Common;
using WhatBug.Domain.Entities.JoinTables;

namespace WhatBug.Domain.Entities.Permissions
{
    public class PermissionScheme : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<PermissionSchemeProjectRolePermission> ProjectRolePermissions = new List<PermissionSchemeProjectRolePermission>();
    }
}
