using System.Collections.Generic;

namespace WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions
{
    public class GetGrantRolePermissionsQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<PermissionDTO> Permissions { get; set; }
    }

    public class PermissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsGranted { get; set; }
    }
}