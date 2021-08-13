using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Projects.Queries.GetAssignUsersToRole
{
    public class GetAssignUsersToRoleQueryHandler : IRequestHandler<GetAssignUsersToRoleQuery, AssignUsersToRolesDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IAuthenticationProvider _authProvider;
        private readonly IMapper _mapper;

        public GetAssignUsersToRoleQueryHandler(IWhatBugDbContext context, IMapper mapper, IAuthenticationProvider authProvider)
        {
            _context = context;
            _mapper = mapper;
            _authProvider = authProvider;
        }

        public async Task<AssignUsersToRolesDTO> Handle(GetAssignUsersToRoleQuery request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var dto = new AssignUsersToRolesDTO
            {
                ProjectId = request.ProjectId,
                Project = await _mapper.ProjectTo<ProjectDTO>(_context.Projects).FirstOrDefaultAsync(p => p.Id == request.ProjectId),
                Roles = await _mapper.ProjectTo<RoleDTO>(_context.Roles).ToListAsync(),
                Users = await _mapper.ProjectTo<UserDTO>(_context.Users).ToListAsync()
            };

            // TODO: Fix this. We should not be querying usernames one by one. This should be replaced with data
            // from the User table but right now the User table is empty so we are using username from the
            // authentication table.

            foreach (var user in dto.Users)
            {
                user.Username = await _authProvider.GetUsername(user.Id);
            }

            return dto;
        }
    }
}
