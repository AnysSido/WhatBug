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

namespace WhatBug.Application.PermissionSchemes.Queries.GetPermissionSchemes
{
    [Authorize(Permissions.ManagePermissionSchemes)]
    public record GetPermissionSchemesQuery : IQuery<Response<GetPermissionSchemesQueryResult>>
    {
    }

    public class GetPermissionSchemesQueryHandler : IRequestHandler<GetPermissionSchemesQuery, Response<GetPermissionSchemesQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetPermissionSchemesQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetPermissionSchemesQueryResult>> Handle(GetPermissionSchemesQuery request, CancellationToken cancellationToken)
        {
            var permissionSchemes = await _context.PermissionSchemes.ProjectTo<PermissionSchemeDTO>(_mapper.ConfigurationProvider).ToListAsync();

            var dto = new GetPermissionSchemesQueryResult
            {
                PermissionSchemes = permissionSchemes.OrderBy(s => !s.IsDefault).ThenBy(s => s.Name).ToList()
            };
            return Response<GetPermissionSchemesQueryResult>.Success(dto);
        }
    }
}