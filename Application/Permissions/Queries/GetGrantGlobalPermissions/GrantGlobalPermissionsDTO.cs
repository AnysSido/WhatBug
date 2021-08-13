using System.Collections.Generic;

namespace WhatBug.Application.Permissions.Queries.GetGrantGlobalPermissions
{
    public class GrantGlobalPermissionsDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public IEnumerable<int> PermissionIds { get; set; }
        public IList<PermissionDTO> Permissions { get; set; }
    }
}
