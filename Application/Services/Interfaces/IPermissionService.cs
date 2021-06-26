using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IPermissionService
    {
        Task SetRolePermissions(SetRolePermissionsDTO setRolePermissionsDTO);
        Task SetUserPermissions(SetUserPermissionDTO setUserGlobalPermissionsDTO);
        Task SetUserProjectRole(SetUserProjectRoleDTO setUserProjectRoleDTO);
        Task<bool> UserHasPermission(int userId, Permission permission);
    }
}
