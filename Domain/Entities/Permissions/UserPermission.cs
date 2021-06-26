using WhatBug.Domain.Common;

namespace WhatBug.Domain.Entities.Permissions
{
    public class UserPermission : AuditableEntity
    {
        public int Id { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
