using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.ProjectRoles.Queries.GetRoles
{
    [Authorize(Permissions.ManageProjectRoles)]
    public record GetRolesQuery : IQuery<Response<GetRolesQueryResult>> { }

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, Response<GetRolesQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetRolesQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetRolesQueryResult>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _context.Roles
                .Include(r => r.ProjectUsers)
                    .ThenInclude(p => p.Project)
                .Include(r => r.ProjectUsers)
                    .ThenInclude(u => u.User)
                .ToListAsync();

            // Roles, projects and users are linked by the 3-way join entity ProjectRoleUsers.
            // This query removes duplicate entities and structures the data in a role > projects > users hierarchy.
            var dto = new GetRolesQueryResult
            {
                Roles = roles.Select(role => new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    Projects = role.ProjectUsers.GroupBy(p => p.ProjectId).Select(grouping => new ProjectDto
                    {
                        Name = grouping.First().Project.Name,
                        Users = grouping.Select(g => g.User).GroupBy(u => u.Id).Select(u => u.First()).ToList().Select(user => new UserDto
                        {
                            FirstName = user.FirstName,
                            Surname = user.Surname,
                            Username = user.Username
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return Response<GetRolesQueryResult>.Success(dto);
        }
    }
}