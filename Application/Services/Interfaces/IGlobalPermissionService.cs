using System.Collections.Generic;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Permissions;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IGlobalPermissionService
    {
        Task<List<PermissionDTO>> GetUserGlobalPermissionsAsync(int userId);
    }
}
