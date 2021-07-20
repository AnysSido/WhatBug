using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Permissions;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IPermissionSchemeService
    {
        Task CreatePermissionScheme(CreatePermissionSchemeDTO dto);
    }
}
