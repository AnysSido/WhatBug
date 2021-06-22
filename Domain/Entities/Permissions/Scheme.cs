using System.Collections.Generic;

namespace WhatBug.Domain.Entities.Permissions
{
    public class Scheme // Default scheme, Software Development scheme etc
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<SchemeRolePermission> RolePermissions = new List<SchemeRolePermission>();
    }
}
