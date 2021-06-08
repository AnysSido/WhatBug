using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.Services.Interfaces;

namespace WhatBug.Application.Services
{
    class ProjectService : IProjectService
    {
        public bool CreateProject(CreateProjectDTO createProjectDTO)
        {
            return true;
        }
    }
}
