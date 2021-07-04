using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IssueDTO> CreateIssue(int projectId, IssueDTO dto);
        public Task CreateProject(CreateProjectDTO createProjectDTO);
        Task<ProjectDTO> GetProject(int id);
        Task<List<Project>> ListProjects();
    }
}
