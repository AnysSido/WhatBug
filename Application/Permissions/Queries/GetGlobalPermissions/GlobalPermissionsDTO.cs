using System.Collections.Generic;

namespace WhatBug.Application.Permissions.Queries.GetGlobalPermissions
{
    public class GlobalPermissionsDTO
    {
        public IList<UserDTO> Users { get; set; }
    }
}
