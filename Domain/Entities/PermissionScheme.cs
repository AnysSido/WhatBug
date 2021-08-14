using System.Collections.Generic;
using WhatBug.Domain.Common;
using WhatBug.Domain.Entities.JoinTables;

namespace WhatBug.Domain.Entities
{
    public class PermissionScheme : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<PermissionSchemeRolePermission> ProjectRolePermissions = new List<PermissionSchemeRolePermission>();
    }
}
