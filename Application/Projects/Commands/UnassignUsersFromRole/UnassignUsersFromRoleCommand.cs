using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;

namespace WhatBug.Application.Projects.Commands.UnassignUsersFromRole
{
    public record UnassignUsersFromRoleCommand : ICommand<Response>
    {
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<int> UserIds { get; set; }
    }

    public class UnassignUsersFromRoleCommandHandler : IRequestHandler<UnassignUsersFromRoleCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public UnassignUsersFromRoleCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(UnassignUsersFromRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.UserIds == null || !request.UserIds.Any())
                return Response.Success();

            var project = await _context.Projects
                .Include(p => p.RoleUsers)
                .Where(p => p.Id == request.ProjectId)
                .FirstOrDefaultAsync();

            project.RoleUsers.RemoveAll(ru => ru.RoleId == request.RoleId && request.UserIds.Contains(ru.UserId));

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}
