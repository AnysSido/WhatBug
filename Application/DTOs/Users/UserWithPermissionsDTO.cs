using System.Collections.Generic;
using WhatBug.Application.DTOs.Permissions;

namespace WhatBug.Application.DTOs.Users
{
    public class UserWithPermissionsDTO
    {
        public UserDTO User { get; set; }
        public List<PermissionDTO> Permissions { get; set; } = new List<PermissionDTO>();
    }
}
