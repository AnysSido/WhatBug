using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.ProjectRoles.Queries.GetEditRole
{
    [Authorize(Permissions.ManageProjectRoles)]
    public record GetEditRoleQuery : IQuery<Response<GetEditRoleQueryResult>>
    {
        public int RoleId { get; init; }
    }

    public class GetEditRoleQueryHandler : IRequestHandler<GetEditRoleQuery, Response<GetEditRoleQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetEditRoleQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetEditRoleQueryResult>> Handle(GetEditRoleQuery request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.Where(r => r.Id == request.RoleId)
                .ProjectTo<GetEditRoleQueryResult>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return Response<GetEditRoleQueryResult>.Success(role);
        }
    }
}