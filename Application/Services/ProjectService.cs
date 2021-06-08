using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Services
{
    class ProjectService : IProjectService
    {
        private readonly IWhatBugDbContext _context;

        public ProjectService(IWhatBugDbContext whatBugDbContext)
        {
            _context = whatBugDbContext;
        }

        public async Task<bool> CreateProject(CreateProjectDTO createProjectDTO)
        {
            var project = new Project()
            {
                Name = createProjectDTO.Name,
                Description = createProjectDTO.Description
            };

            _context.Projects.Add(project);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
