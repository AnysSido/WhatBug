using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Admin;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IAdminService
    {
        Task CreateProjectRole(CreateProjectRoleDTO dto);
        Task<List<ProjectRoleDTO>> GetProjectRolesAsync();
    }
}
