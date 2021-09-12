﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Models;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Admin.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public CreateRoleCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role { Name = request.Name, Description = request.Description };
            _context.Roles.Add(role);
            
            await _context.SaveChangesAsync(cancellationToken);

            return Response.Success();
        }
    }
}
