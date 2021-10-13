namespace WhatBug.Domain.Entities
{
    public class PermissionSchemeRolePermission
    {
        public int PermissionSchemeId { get; set; }
        public PermissionScheme PermissionScheme { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}