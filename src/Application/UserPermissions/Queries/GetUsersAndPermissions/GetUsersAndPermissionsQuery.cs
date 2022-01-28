using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.UserPermissions.Queries.GetUsersAndPermissions
{
    [Authorize(Permissions.ManageUserPermissions)]
    public record GetUsersAndPermissionsQuery : IQuery<Response<GetUsersAndPermissionsQueryResult>> { }

    public class GetUsersAndPermissionsQueryHandler : IRequestHandler<GetUsersAndPermissionsQuery, Response<GetUsersAndPermissionsQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersAndPermissionsQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetUsersAndPermissionsQueryResult>> Handle(GetUsersAndPermissionsQuery request, CancellationToken cancellationToken)
        {
            var dto = new GetUsersAndPermissionsQueryResult
            {
                Users = await _mapper.ProjectTo<UserDTO>(_context.Users).ToListAsync()
            };

            return Response<GetUsersAndPermissionsQueryResult>.Success(dto);
        }
    }
}
