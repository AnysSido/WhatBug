using System.Collections.Generic;

namespace WhatBug.Application.UserPermissions.Queries.GetGrantUserPermissions
{
    public class GetGrantUserPermissionsQueryResult
    {
        public int Id { get; set; }
        public string Username { get; set; }
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