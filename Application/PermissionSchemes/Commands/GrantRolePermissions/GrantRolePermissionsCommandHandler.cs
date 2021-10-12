using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Commands.GrantRolePermissions
{
    public class GrantRolePermissionsCommandHandler : IRequestHandler<GrantRolePermissionsCommand>
    {
        private readonly IWhatBugDbContext _context;

        public GrantRolePermissionsCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(GrantRolePermissionsCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var scheme = await _context.PermissionSchemes.Include(s => s.ProjectRolePermissions).FirstAsync(s => s.Id == request.SchemeId);
            var role = await _context.Roles.FirstAsync(r => r.Id == request.RoleId);
            var permissions = await _context.Permissions.Where(p => request.PermissionIds.Contains(p.Id)).ToListAsync();

            var grantedPermissions = permissions.Select(p => new PermissionSchemeRolePermission
            {
                PermissionScheme = scheme,
                Role = role, Permission = p
            });

            scheme.ProjectRolePermissions.RemoveAll(p => p.RoleId == role.Id);
            scheme.ProjectRolePermissions.AddRange(grantedPermissions);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
