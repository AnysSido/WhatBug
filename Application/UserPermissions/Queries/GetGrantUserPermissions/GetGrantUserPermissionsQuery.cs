using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.UserPermissions.Queries.GetGrantUserPermissions
{
    [Authorize(Permissions.ManageUserPermissions)]
    public record GetGrantUserPermissionsQuery : IQuery<Response<GetGrantUserPermissionsQueryResult>>
    {
        public int UserId { get; set; }
    }

    public class GetGrantUserPermissionsQueryHandler : IRequestHandler<GetGrantUserPermissionsQuery, Response<GetGrantUserPermissionsQueryResult>>
    {
        public IWhatBugDbContext _context;

        public GetGrantUserPermissionsQueryHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<GetGrantUserPermissionsQueryResult>> Handle(GetGrantUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var allGlobalPermissions = await _context.Permissions
                .Where(p => p.Type == PermissionType.Global)
                .ToListAsync();

            var user = await _context.Users
                .Include(u => u.UserPermissions)
                    .ThenInclude(u => u.Permission)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            var dto = new GetGrantUserPermissionsQueryResult
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                Surname = user.Surname,
                Permissions = allGlobalPermissions.Select(globalPermission => new PermissionDTO
                {
                    Id = globalPermission.Id,
                    Description = globalPermission.Description,
                    Name = globalPermission.Name,
                    IsGranted = user.UserPermissions.Select(p => p.Permission.Id).Contains(globalPermission.Id)
                }).ToList()
            };

            return Response<GetGrantUserPermissionsQueryResult>.Success(dto);
        }
    }
}