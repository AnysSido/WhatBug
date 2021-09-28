using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var roles = await _context.Roles.ProjectTo<RoleDto>(_mapper.ConfigurationProvider).ToListAsync();
            var result = new GetRolesQueryResult { Roles = roles };

            return Response<GetRolesQueryResult>.Success(result);
        }
    }
}