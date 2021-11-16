using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Projects.Queries.GetAssignUsersToRole
{
    //[Authorize(Permissions.AssignUserRoles)]
    public record GetAssignUsersToRoleQuery : IQuery<Response<GetAssignUsersToRoleQueryResult>>, IRequireProjectAuthorization
    {
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
    }

    public class GetAssignUsersToRoleQueryHandler : IRequestHandler<GetAssignUsersToRoleQuery, Response<GetAssignUsersToRoleQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetAssignUsersToRoleQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetAssignUsersToRoleQueryResult>> Handle(GetAssignUsersToRoleQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync();
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId);
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId);

            var projectRoleUsers = await _context.ProjectRoleUsers
                .Include(p => p.Project)
                .Include(r => r.Role)
                .Include(u => u.User)
                .Where(p => p.ProjectId == request.ProjectId)
                .Where(r => r.RoleId == request.RoleId)
                .ToListAsync();

            var usersInRole = projectRoleUsers.Select(u => u.User);
            var availableUsers = users.Where(u => !usersInRole.Select(ru => ru.Id).Contains(u.Id));

            var dto = new GetAssignUsersToRoleQueryResult
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                RoleId = role.Id,
                RoleName = role.Name,
                AvailableUsers = _mapper.Map<List<UserDTO>>(availableUsers),
                UsersInRole = _mapper.Map<List<UserDTO>>(usersInRole),
            };

            return Response<GetAssignUsersToRoleQueryResult>.Success(dto);
        }
    }
}
