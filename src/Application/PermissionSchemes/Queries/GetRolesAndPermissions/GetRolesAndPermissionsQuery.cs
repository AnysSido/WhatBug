using AutoMapper;
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

namespace WhatBug.Application.PermissionSchemes.Queries.GetRolesAndPermissions
{
    [Authorize(Permissions.ManagePermissionSchemes)]
    public record GetRolesAndPermissionsQuery : IQuery<Response<GetRolesAndPermissionsQueryResult>>
    {
        public int SchemeId { get; set; }
    }

    public class GetRolesAndPermissionsQueryHandler : IRequestHandler<GetRolesAndPermissionsQuery, Response<GetRolesAndPermissionsQueryResult>>
    {
        private readonly IWhatBugDbContext _context;

        public GetRolesAndPermissionsQueryHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<GetRolesAndPermissionsQueryResult>> Handle(GetRolesAndPermissionsQuery request, CancellationToken cancellationToken)
        {
            var allRoles = await _context.Roles.ToListAsync();

            var permissionScheme = await _context.PermissionSchemes
                .Include(s => s.RolePermissions)
                    .ThenInclude(r => r.Role)
                .Include(s => s.RolePermissions)
                    .ThenInclude(p => p.Permission)
                .FirstAsync(s => s.Id == request.SchemeId);

            var dto = new GetRolesAndPermissionsQueryResult
            {
                SchemeId = permissionScheme.Id,
                Name = permissionScheme.Name,
                Roles = permissionScheme.RolePermissions.GroupBy(p => p.RoleId).Select(grouping => new RoleDto
                {
                    Id = grouping.Key,
                    Name = grouping.First().Role.Name,
                    Description = grouping.First().Role.Description,
                    Permissions = grouping.Select(g => new PermissionDto
                    {
                        Id = g.Permission.Id,
                        Name = g.Permission.Name,
                        Description = g.Permission.Description
                    }).ToList()
                }).ToList()
            };

            var rolesWithoutPermissions = allRoles.Where(r => !dto.Roles.Any(role => role.Id == r.Id)).ToList();
            rolesWithoutPermissions.ForEach(r => dto.Roles.Add(new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Permissions = new List<PermissionDto>()
            }));

            dto.Roles = dto.Roles.OrderBy(r => r.Name).ToList();

            return Response<GetRolesAndPermissionsQueryResult>.Success(dto);
        }
    }
}