using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.DTOs.Users;
using WhatBug.Application.Services.Interfaces;

namespace WhatBug.Application.Services
{
    class ProjectService : IProjectService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly IMapper _mapper;

        public ProjectService(IWhatBugDbContext whatBugDbContext, IMapper mapper, IAuthenticationProvider authenticationProvider)
        {
            _context = whatBugDbContext;
            _mapper = mapper;
            _authenticationProvider = authenticationProvider;
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

        public async Task<List<UserDTO>> GetProjectUsersAsync(int projectId)
        {
            //// TODO: Check permission

            var projectUsers = await _context.Projects
                .Where(p => p.Id == projectId)
                .Include(p => p.RoleUsers)
                    .ThenInclude(p => p.User)
                .SelectMany(p => p.RoleUsers.Select(p => p.User))
                .ToListAsync();

            projectUsers = projectUsers.GroupBy(u => u.Id).Select(u => u.First()).ToList();
            var dtos = await _authenticationProvider.PopulatePrincipleUsersInfo(_mapper.Map<List<UserDTO>>(projectUsers));

            return dtos.ToList();
        }
    }
}
