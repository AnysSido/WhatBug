using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Result;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme
{
    public class CreatePermissionSchemeCommandHandler : IRequestHandler<CreatePermissionSchemeCommand, Result>
    {
        private readonly IWhatBugDbContext _context;

        public CreatePermissionSchemeCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreatePermissionSchemeCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            bool schemeNameExists = await _context.PermissionSchemes.AnyAsync(s => s.Name == request.Name);

            if (schemeNameExists)
                return Result.Failure(Errors.PermissionScheme.NameIsTaken(request.Name));

            _context.PermissionSchemes.Add(new PermissionScheme { Name = request.Name, Description = request.Description });
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
