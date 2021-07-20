using WhatBug.Domain.Common;

namespace WhatBug.Domain.Entities.Permissions
{
    public class RolePermission : AuditableEntity
    {
        public int Id { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int SchemeId { get; set; }        
        public PermissionScheme PermissionScheme { get; set; }
    }
}
