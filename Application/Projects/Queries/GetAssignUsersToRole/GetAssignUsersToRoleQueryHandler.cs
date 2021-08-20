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
        private readonly IMapper _mapper;

        public GetAssignUsersToRoleQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            return dto;
        }
    }
}
