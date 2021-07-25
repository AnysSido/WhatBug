using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.Users;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IPermissionService
    {
        List<PermissionDTO> GetAllPermissions(PermissionType permissionType);
        Task<List<PermissionDTO>> GetUserPermissions(int userId);
        Task SetRolePermissions(SetRolePermissionsDTO setRolePermissionsDTO);
        Task SetUserPermissions(SetUserPermissionsDTO setUserGlobalPermissionsDTO);
        Task SetUserProjectRole(SetUserProjectRoleDTO setUserProjectRoleDTO);
        Task<bool> UserHasPermission(int userId, string permission);
    }
}
