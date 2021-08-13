using System.Collections.Generic;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.DTOs.Users;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDTO>> GetAllProjects();
        Task<ProjectDTO> GetProjectAsync(int id);
        Task<List<UserDTO>> GetProjectUsersAsync(int projectId);
    }
}
