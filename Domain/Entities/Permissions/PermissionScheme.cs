using System.Collections.Generic;
using WhatBug.Domain.Common;

namespace WhatBug.Domain.Entities.Permissions
{
    public class PermissionScheme : AuditableEntity // Default scheme, Software Development scheme etc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<RolePermission> RolePermissions = new List<RolePermission>();
    }
}
