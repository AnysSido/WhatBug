using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace WhatBug.Application.Projects.Queries.GetUsersAndRoles
{
    //[Authorize(PermissionOperator.Or, Permissions.ViewProjectMembers, Permissions.ManageUserRoles)]
    public record GetUsersAndRolesQuery : IQuery<Response<GetUsersAndRolesQueryResult>>, IRequireProjectAuthorization
    {
        public int ProjectId { get; set; }
    }

    public class GetUsersAndRolesQueryHandler : IRequestHandler<GetUsersAndRolesQuery, Response<GetUsersAndRolesQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersAndRolesQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetUsersAndRolesQueryResult>> Handle(GetUsersAndRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _context.Roles
                .ProjectTo<ProjectRoleDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            var projectRoleUsers = await _context.ProjectRoleUsers
                .Include(u => u.User)
                .Include(r => r.Role)
                .Where(p => p.ProjectId == request.ProjectId)
                .ToListAsync();

            var dto = await _context.Projects
                .Where(p => p.Id == request.ProjectId)
                .ProjectTo<GetUsersAndRolesQueryResult>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            roles.ForEach(r =>
            {
                r.Users = _mapper.Map<List<UserDTO>>(projectRoleUsers.Where(role => role.RoleId == r.Id).Select(r => r.User));
            });

            dto.Roles = roles;

            return Response<GetUsersAndRolesQueryResult>.Success(dto);
        }
    }
}
