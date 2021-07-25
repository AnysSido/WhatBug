using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.GlobalPermissions;
using WhatBug.Application.DTOs.Permissions;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IGlobalPermissionService
    {
        List<PermissionDTO> GetAvailableGlobalPermissions();
        Task<List<PermissionDTO>> GetUserGlobalPermissionsAsync(int userId);
        Task SetGlobalPermissionsAsync(SetGlobalPermissionsDTO dto);
    }
}
