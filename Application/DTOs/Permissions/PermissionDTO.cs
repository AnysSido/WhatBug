using WhatBug.Domain.Entities;

namespace WhatBug.Application.DTOs.Permissions
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PermissionType Type { get; set; }
    }
}
