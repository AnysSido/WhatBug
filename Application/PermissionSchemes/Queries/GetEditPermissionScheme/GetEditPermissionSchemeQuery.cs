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

namespace WhatBug.Application.PermissionSchemes.Queries.GetEditPermissionScheme
{
    [Authorize(Permissions.ManageProjectRoles)]
    public record GetEditPermissionSchemeQuery : IQuery<Response<GetEditPermissionSchemeQueryResult>>
    {
        public int SchemeId { get; set; }
    }

    public class GetEditPermissionSchemeQueryHandler : IRequestHandler<GetEditPermissionSchemeQuery, Response<GetEditPermissionSchemeQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetEditPermissionSchemeQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetEditPermissionSchemeQueryResult>> Handle(GetEditPermissionSchemeQuery request, CancellationToken cancellationToken)
        {
            var permissionScheme = await _context.PermissionSchemes.Where(s => s.Id == request.SchemeId)
                .ProjectTo<GetEditPermissionSchemeQueryResult>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return Response<GetEditPermissionSchemeQueryResult>.Success(permissionScheme);
        }
    }
}