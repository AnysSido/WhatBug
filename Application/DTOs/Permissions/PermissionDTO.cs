using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Application.DTOs.Users
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PermissionType Type { get; set; }
    }
}
