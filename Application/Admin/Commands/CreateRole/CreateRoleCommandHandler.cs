using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Result;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Admin.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result>
    {
        private readonly IWhatBugDbContext _context;

        public CreateRoleCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole = await _context.Roles.AnyAsync(r => r.Name == request.Name);

            if (existingRole)
                return Result.Failure(Errors.Admin.Roles.NameIsTaken(request.Name));

            var role = new Role { Name = request.Name, Description = request.Description };
            _context.Roles.Add(role);
            
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
