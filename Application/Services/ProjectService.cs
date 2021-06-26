using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Exceptions;

namespace WhatBug.Application.Services
{
    class ProjectService : IProjectService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IPermissionService _permissionService;
        private readonly ICurrentUserService _currentUserService;

        public ProjectService(IWhatBugDbContext whatBugDbContext, IPermissionService permissionService, ICurrentUserService currentUserService)
        {
            _context = whatBugDbContext;
            _permissionService = permissionService;
            _currentUserService = currentUserService;
        }

        public async Task CreateProject(CreateProjectDTO createProjectDTO)
        {
            if (!await _permissionService.UserHasPermission(_currentUserService.UserId, Permissions.CreateProject))
                throw new InsufficientPermissionException(_currentUserService.UserId, Permissions.CreateProject);

            var project = new Project()
            {
                Name = createProjectDTO.Name,
                Description = createProjectDTO.Description
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Project>> ListProjects()
        {
            return await _context.Projects.ToListAsync();
        }
    }
}
