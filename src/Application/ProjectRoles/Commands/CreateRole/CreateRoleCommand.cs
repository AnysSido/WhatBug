using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.ProjectRoles.Commands.CreateRole
{
    [Authorize(Permissions.ManageProjectRoles)]
    public record CreateRoleCommand : ICommand<Response<int>>
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Response<int>>
    {
        private readonly IWhatBugDbContext _context;

        public CreateRoleCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role { Name = request.Name, Description = request.Description };
            _context.Roles.Add(role);

            await _context.SaveChangesAsync(cancellationToken);

            return Response<int>.Success(role.Id);
        }
    }
}