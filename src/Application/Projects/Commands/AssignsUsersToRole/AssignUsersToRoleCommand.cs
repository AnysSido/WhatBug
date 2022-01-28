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

namespace WhatBug.Application.Projects.Commands.AssignsUsersToRole
{
    [Authorize(Permissions.AssignUserRoles)]
    public record AssignUsersToRoleCommand : ICommand<Response>, IRequireProjectAuthorization
    {
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<int> UserIds { get; set; }
    }

    public class AssignUsersToRoleCommandHandler : IRequestHandler<AssignUsersToRoleCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public AssignUsersToRoleCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(AssignUsersToRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.UserIds == null || !request.UserIds.Any())
                return Response.Success();

            var role = await _context.Roles
                .Where(r => r.Id == request.RoleId)
                .SingleOrDefaultAsync();

            var project = await _context.Projects
                .Include(p => p.RoleUsers)
                .Where(p => p.Id == request.ProjectId)
                .FirstOrDefaultAsync();

            var existingUserIds = project.RoleUsers
                .Where(r => r.RoleId == role.Id)
                .Where(u => request.UserIds.Contains(u.UserId))
                .Select(u => u.UserId)
                .ToList();

            var users = await _context.Users
                .Where(u => request.UserIds.Contains(u.Id))
                .Where(u => !existingUserIds.Contains(u.Id))
                .ToListAsync();

            users.ForEach(user =>
            {
                project.RoleUsers.Add(new ProjectRoleUser
                {
                    Role = role,
                    User = user,
                });
            });

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}
