using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Issues;
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
        private readonly IMapper _mapper;

        public ProjectService(IWhatBugDbContext whatBugDbContext, IPermissionService permissionService, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context = whatBugDbContext;
            _permissionService = permissionService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task CreateProject(CreateProjectDTO createProjectDTO)
        {
            if (!await _permissionService.UserHasPermission(_currentUserService.UserId, Permissions.CreateProject))
                throw new InsufficientPermissionException(_currentUserService.UserId, Permissions.CreateProject);

            var project = new Project()
            {
                Name = createProjectDTO.Name,
                Description = createProjectDTO.Description,
                PrioritySchemeId = createProjectDTO.PrioritySchemeId
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Project>> ListProjects()
        {
            if (await _permissionService.UserHasPermission(_currentUserService.UserId, Permissions.ViewAllProjects))
                return await _context.Projects.ToListAsync();

            return await _context.ProjectRoleUsers
                .Include(r => r.Project)
                .Include(r => r.User)
                .Where(r => r.UserId == _currentUserService.UserId)
                .Select(r => r.Project)
                .ToListAsync();
        }

        public async Task<ProjectDTO> GetProject(int id)
        {
            // TODO: Check permission

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<IssueDTO> CreateIssue(int projectId, IssueDTO dto)
        {
            // TODO: Check permission

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            project.Issues.Add(_mapper.Map<Issue>(dto));
            await _context.SaveChangesAsync();
            return dto;
        }
    }
}
