using System.Collections.Generic;

namespace WhatBug.Application.PermissionSchemes.Queries.GetRolesAndPermissions
{
    public class GetRolesAndPermissionsQueryResult
    {
        public int SchemeId { get; set; }
        public string Name { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }

    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<PermissionDto> Permissions { get; set; }
    }

    public class PermissionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}