using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Application.ProjectRoles.Queries.GetDeleteRole;
using WhatBug.Domain.Data;

namespace WhatBug.Application.ProjectRoles.Queries.GetDeleteRoleConfirm
{
    [Authorize(Permissions.ManageProjectRoles)]
    public record GetDeleteRoleConfirmQuery : IQuery<Response<GetDeleteRoleConfirmQueryResult>>
    {
        public int RoleId { get; init; }
    }

    public class GetDeleteRoleConfirmQueryHandler : IRequestHandler<GetDeleteRoleConfirmQuery, Response<GetDeleteRoleConfirmQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetDeleteRoleConfirmQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetDeleteRoleConfirmQueryResult>> Handle(GetDeleteRoleConfirmQuery request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles
                .Include(r => r.ProjectUsers)
                    .ThenInclude(p => p.Project)
                .Include(r => r.ProjectUsers)
                    .ThenInclude(u => u.User)
                .Where(r => r.Id == request.RoleId)
                .FirstAsync();

            role.ProjectUsers = role.ProjectUsers.GroupBy(p => p.ProjectId).Select(p => p.First()).ToList();

            var dto = _mapper.Map<GetDeleteRoleConfirmQueryResult>(role);

            return Response<GetDeleteRoleConfirmQueryResult>.Success(dto);
        }
    }
}