using System.Collections.Generic;
using WhatBug.Domain.Common;

namespace WhatBug.Domain.Entities
{
    public class PermissionScheme : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }

        public List<PermissionSchemeRolePermission> RolePermissions = new List<PermissionSchemeRolePermission>();
    }
}
