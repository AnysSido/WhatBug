﻿namespace WhatBug.Domain.Entities.Permissions
{
    public class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int SchemeId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
        public Scheme Scheme { get; set; } // Null for global role permissions
    }
}
