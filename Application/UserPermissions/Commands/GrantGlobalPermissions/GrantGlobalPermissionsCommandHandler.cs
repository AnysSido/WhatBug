using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities.JoinTables;

namespace WhatBug.Application.UserPermissions.Commands.GrantGlobalPermissions
{
    public class GrantGlobalPermissionsCommandHandler : IRequestHandler<GrantGlobalPermissionsCommand>
    {
        private readonly IWhatBugDbContext _context;

        public GrantGlobalPermissionsCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(GrantGlobalPermissionsCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var user = await _context.Users.Include(u => u.UserPermissions).ThenInclude(u => u.Permission)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            var permissionsToGrant = await _context.Permissions.Where(p => request.PermissionIds.Contains(p.Id)).ToListAsync();
            user.UserPermissions.Clear();
            user.UserPermissions.AddRange(permissionsToGrant.Select(p => new UserPermission { User = user, Permission = p }));

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
