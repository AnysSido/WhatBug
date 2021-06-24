using System.Collections.Generic;

namespace WhatBug.Domain.Entities.Permissions
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}