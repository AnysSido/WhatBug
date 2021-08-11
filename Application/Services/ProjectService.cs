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
using WhatBug.Application.DTOs.Users;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.JoinTables;
using WhatBug.Domain.Exceptions;

namespace WhatBug.Application.Services
{
    class ProjectService : IProjectService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IPermissionService _permissionService;
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public ProjectService(IWhatBugDbContext whatBugDbContext, IPermissionService permissionService, ICurrentUserService currentUserService, IMapper mapper, IAuthenticationProvider authenticationProvider)
        {
            _context = whatBugDbContext;
            _permissionService = permissionService;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _authenticationProvider = authenticationProvider;
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

        public async Task<ProjectDTO> GetProjectAsync(int id)
        {
            // TODO: Check permission

            var project = await _context.Projects
                .Include(p => p.PriorityScheme.Priorities).ThenInclude(p => p.ColorIcon.Color)
                .Include(p => p.PriorityScheme.Priorities).ThenInclude(p => p.ColorIcon.Icon)
                .FirstOrDefaultAsync(p => p.Id == id);
            project.PriorityScheme.Priorities.Sort((a, b) => a.Order.CompareTo(b.Order));
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<List<ProjectDTO>> GetAllProjects()
        {
            // TODO: Check permissions
            return _mapper.Map<List<ProjectDTO>>(await _context.Projects
                .Include(p => p.PriorityScheme.Priorities).ThenInclude(p => p.ColorIcon.Color)
                .Include(p => p.PriorityScheme.Priorities).ThenInclude(p => p.ColorIcon.Icon)
                .ToListAsync());
        }

        public async Task<List<ProjectRoleWithUsersDTO>> GetProjectRolesWithUsersAsync(int projectId)
        {
            // TODO: Check permissions

            var project = await _context.Projects
                .Include(p => p.ProjectRoleUsers).ThenInclude(p => p.User)
                .Include(p => p.ProjectRoleUsers).ThenInclude(p => p.ProjectRole)
                .SingleAsync(p => p.Id == projectId);

            var rolesWithUsers = project.ProjectRoleUsers
                .GroupBy(r => r.ProjectRole)
                .SelectMany(r => new List<ProjectRoleWithUsersDTO> {
                    new ProjectRoleWithUsersDTO
                    {
                        RoleId = r.Key.Id,
                        RoleName = r.Key.Name,
                        Users = r.Select(u => _mapper.Map<UserDTO>(u.User)).ToList()
                    }
                }).ToList();

            foreach (var role in rolesWithUsers)
                await _authenticationProvider.PopulatePrincipleUsersInfo(role.Users);

            return rolesWithUsers;
        }

        public async Task AddUsersToProjectRoleAsync(AddUsersToProjectRoleDTO dto)
        {
            // TODO: Check permission

            var project = await _context.Projects
                .Include(p => p.ProjectRoleUsers).ThenInclude(p => p.User)
                .Include(p => p.ProjectRoleUsers).ThenInclude(p => p.ProjectRole)
                .SingleAsync(p => p.Id == dto.ProjectId);

            var projectRole = await _context.ProjectRoles.SingleAsync(r => r.Id == dto.ProjectRoleId);
            var users = await _context.Users.Where(u => dto.UserIds.Contains(u.Id)).ToListAsync();

            users.ForEach(u => project.ProjectRoleUsers.Add(new ProjectUserProjectRole
            {
                User = u,
                ProjectRole = projectRole
            }));

            await _context.SaveChangesAsync();
        }

        public async Task<List<UserDTO>> GetProjectUsersAsync(int projectId)
        {
            // TODO: Check permission

            var projectUsers = await _context.Projects
                .Where(p => p.Id == projectId)
                .Include(p => p.ProjectRoleUsers)
                    .ThenInclude(p => p.User)
                .SelectMany(p => p.ProjectRoleUsers.Select(p => p.User))
                .ToListAsync();

            projectUsers = projectUsers.GroupBy(u => u.Id).Select(u => u.First()).ToList();
            var dtos = await _authenticationProvider.PopulatePrincipleUsersInfo(_mapper.Map<List<UserDTO>>(projectUsers));

            return dtos.ToList();
        }
    }
}
