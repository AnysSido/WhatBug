using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.DTOs.Users;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IProjectService
    {
        Task AddUsersToProjectRoleAsync(AddUsersToProjectRoleDTO dto);
        public Task CreateProject(CreateProjectDTO createProjectDTO);
        Task<List<ProjectDTO>> GetAllProjects();
        Task<ProjectDTO> GetProjectAsync(int id);
        Task<List<ProjectRoleWithUsersDTO>> GetProjectRolesWithUsersAsync(int projectId);
        Task<List<UserDTO>> GetProjectUsersAsync(int projectId);
    }
}
