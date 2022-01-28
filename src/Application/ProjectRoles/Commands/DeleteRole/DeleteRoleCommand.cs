using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.ProjectRoles.Commands.DeleteRole
{
    [Authorize(Permissions.ManageProjectRoles)]
    public record DeleteRoleCommand : ICommand<Response>
    {
        public int RoleId { get; init; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public DeleteRoleCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FirstAsync(r => r.Id == request.RoleId);

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}