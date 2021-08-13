using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Commands.AssignsUsersToRole
{
    public class AssignUsersToRoleCommandHandler : IRequestHandler<AssignUsersToRoleCommand>
    {
        private readonly IWhatBugDbContext _context;

        public AssignUsersToRoleCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AssignUsersToRoleCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.Where(p => p.Id == request.ProjectId).SingleOrDefaultAsync();
            var users = await _context.Users.Where(u => request.UserIds.Contains(u.Id)).ToListAsync();
            var role = await _context.Roles.Include(r => r.ProjectUsers).Where(r => r.Id == request.RoleId).SingleOrDefaultAsync();

            // TODO: Check if user already has role

            users.ForEach(user =>
            {
                role.ProjectUsers.Add(new ProjectRoleUser
                {
                    Project = project,
                    Role = role,
                    User = user
                });
            });

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
