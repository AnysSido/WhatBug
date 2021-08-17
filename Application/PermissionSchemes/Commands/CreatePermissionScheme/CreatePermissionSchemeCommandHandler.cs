using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme
{
    public class CreatePermissionSchemeCommandHandler : IRequestHandler<CreatePermissionSchemeCommand>
    {
        private readonly IWhatBugDbContext _context;

        public CreatePermissionSchemeCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreatePermissionSchemeCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var existingScheme = _context.PermissionSchemes.FirstOrDefaultAsync(s => s.Name == request.Name);

            if (existingScheme != null)
            { 
                // TODO: Throw exception
            }

            _context.PermissionSchemes.Add(new PermissionScheme { Name = request.Name, Description = request.Description });
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
