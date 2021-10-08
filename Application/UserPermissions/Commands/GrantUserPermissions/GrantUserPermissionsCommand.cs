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
using WhatBug.Domain.Entities.JoinTables;

namespace WhatBug.Application.UserPermissions.Commands.GrantUserPermissions
{
    [Authorize(Permissions.EditUserPermissions)]
    public record GrantUserPermissionsCommand : ICommand<Response>
    {
        public int UserId { get; set; }
        public IEnumerable<int> PermissionIds { get; set; }
    }

    public class GrantUserPermissionsCommandHandler : IRequestHandler<GrantUserPermissionsCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public GrantUserPermissionsCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(GrantUserPermissionsCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.UserPermissions)
                    .ThenInclude(u => u.Permission)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            user.UserPermissions.Clear();

            if (request.PermissionIds?.Any() == true)
            {
                var permissionsToGrant = await _context.Permissions
                    .Where(p => (request.PermissionIds ?? new List<int>()).Contains(p.Id))
                    .ToListAsync();

                user.UserPermissions.AddRange(permissionsToGrant.Select(p => new UserPermission { User = user, Permission = p }));
            }

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}