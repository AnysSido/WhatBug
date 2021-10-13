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
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions
{
    [Authorize(Permissions.ManagePermissionSchemes)]
    public record GetGrantRolePermissionsQuery : IQuery<Response<GetGrantRolePermissionsQueryResult>>
    {
        public int SchemeId { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<int> PermissionIds { get; set; }
    }

    public class GetGrantRolePermissionsQueryHandler : IRequestHandler<GetGrantRolePermissionsQuery, Response<GetGrantRolePermissionsQueryResult>>
    {
        private readonly IWhatBugDbContext _context;

        public GetGrantRolePermissionsQueryHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<GetGrantRolePermissionsQueryResult>> Handle(GetGrantRolePermissionsQuery request, CancellationToken cancellationToken)
        {
            var scheme = await _context.PermissionSchemes.Include(p => p.RolePermissions).Where(s => s.Id == request.SchemeId).FirstAsync();
            var role = await _context.Roles.Where(r => r.Id == request.RoleId).FirstAsync();
            var allPermissions = await _context.Permissions.Where(p => p.Type == PermissionType.Project).ToListAsync();

            var dto = new GetGrantRolePermissionsQueryResult
            {
                Id = scheme.Id,
                Name = scheme.Name,
                Description = scheme.Description,
                RoleId = role.Id,
                RoleName = role.Name,
                Permissions = allPermissions.Select(p => new PermissionDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    IsGranted = scheme.RolePermissions.Where(r => r.RoleId == role.Id).Select(r => r.PermissionId).Contains(p.Id)
                }).ToList()
            };

            return Response<GetGrantRolePermissionsQueryResult>.Success(dto);
        }
    }
}