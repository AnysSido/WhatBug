using System.Collections.Generic;

namespace WhatBug.Application.DTOs.Users
{
    public class UserWithPermissionsDTO
    {
        public UserDTO User { get; set; }
        public List<PermissionDTO> Permissions { get; set; }
    }
}
