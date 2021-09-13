using System.Collections.Generic;

namespace WhatBug.Application.UserPermissions.Queries.GetGlobalPermissions
{
    public class GlobalPermissionsDTO
    {
        public IList<UserDTO> Users { get; set; }
    }
}
