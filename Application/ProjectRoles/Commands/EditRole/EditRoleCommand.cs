using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.ProjectRoles.Commands.EditRole
{
    [Authorize(Permissions.ManageProjectRoles)]
    public record EditRoleCommand : ICommand<Response>
    {
        public int RoleId { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public EditRoleCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.Where(r => r.Id == request.RoleId).FirstOrDefaultAsync();

            role.Name = request.Name;
            role.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : null;

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}