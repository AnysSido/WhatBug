using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Commands.GrantRolePermissions
{
    [Authorize(Permissions.ManagePermissionSchemes)]
    public record GrantRolePermissionsCommand : ICommand<Response>
    {
        public int SchemeId { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<int> PermissionIds { get; set; }
    }

    public class GrantRolePermissionsCommandHandler : IRequestHandler<GrantRolePermissionsCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public GrantRolePermissionsCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(GrantRolePermissionsCommand request, CancellationToken cancellationToken)
        {
            var scheme = await _context.PermissionSchemes.Include(s => s.RolePermissions).FirstAsync(s => s.Id == request.SchemeId);
            var role = await _context.Roles.FirstAsync(r => r.Id == request.RoleId);
            var permissions = await _context.Permissions.Where(p => request.PermissionIds.Contains(p.Id)).ToListAsync();

            var grantedPermissions = permissions.Select(permission => new PermissionSchemeRolePermission
            {
                PermissionScheme = scheme,
                Role = role,
                Permission = permission
            });

            scheme.RolePermissions.RemoveAll(p => p.RoleId == role.Id);
            scheme.RolePermissions.AddRange(grantedPermissions);

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}