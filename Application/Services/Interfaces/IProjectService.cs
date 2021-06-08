using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Projects;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IProjectService
    {
        public Task<bool> CreateProject(CreateProjectDTO createProjectDTO);
    }
}
