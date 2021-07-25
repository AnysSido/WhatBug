using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.PermissionSchemes;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IPermissionSchemeService
    {
        Task CreatePermissionSchemeAsync(CreatePermissionSchemeDTO dto);
        Task<PermissionSchemeDTO> GetPermissionSchemeAsync(int id);
        Task<List<PermissionSchemeDTO>> GetPermissionSchemesAsync();
        Task<List<PermissionDTO>> GetProjectRolePermissionsAsync(int schemeId, int projectRoleId);
        Task SetProjectRolePermissionsAsync(SetProjectRolePermissionsDTO dto);
    }
}
