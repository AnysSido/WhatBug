using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.Permissions;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Services
{
    class ProjectService : IProjectService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public ProjectService(IWhatBugDbContext whatBugDbContext, IAuthorizationService authorizationService)
        {
            _context = whatBugDbContext;
            _authorizationService = authorizationService;
        }

        public async Task<Result> CreateProject(CreateProjectDTO createProjectDTO)
        {
            if (!await _authorizationService.UserHasClaimAsync(createProjectDTO.OwnerId, ClaimType.Project.Add))
                return Result.Failure(new string[] { $"User {createProjectDTO.OwnerId} does not have permission to create a project." });

            var project = new Project()
            {
                Name = createProjectDTO.Name,
                Description = createProjectDTO.Description
            };

            await _context.Projects.AddAsync(project);
            if (await _context.SaveChangesAsync() > 0)
                return Result.Success();

            return Result.Failure(new string[] { $"Unable to create project {createProjectDTO.Name}. Could not update database." });
        }

        public async Task<List<Project>> ListProjects()
        {
            return await _context.Projects.ToListAsync();
        }
    }
}
