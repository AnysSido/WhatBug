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

namespace WhatBug.Application.PrioritySchemes.Commands.CreatePriorityScheme
{
    public class CreatePrioritySchemeCommandHandler : IRequestHandler<CreatePrioritySchemeCommand>
    {
        private readonly IWhatBugDbContext _context;

        public CreatePrioritySchemeCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreatePrioritySchemeCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var priorities = await _context.Priorities.Where(p => request.PriorityIds.Contains(p.Id)).ToListAsync();
            var scheme = new PriorityScheme
            {
                Name = request.Name,
                Description = request.Description,
                Priorities = priorities
            };

            _context.PrioritySchemes.Add(scheme);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
