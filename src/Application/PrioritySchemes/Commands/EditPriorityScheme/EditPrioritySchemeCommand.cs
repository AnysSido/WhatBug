using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PrioritySchemes.Commands.EditPriorityScheme
{
    [Authorize(Permissions.ManagePrioritySchemes)]
    public record EditPrioritySchemeCommand : ICommand<Response>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public IEnumerable<int> PriorityIds { get; init; }
    }

    public class EditPrioritySchemeCommandHandler : IRequestHandler<EditPrioritySchemeCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public EditPrioritySchemeCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(EditPrioritySchemeCommand request, CancellationToken cancellationToken)
        {
            var scheme = await _context.PrioritySchemes
                .Include(s => s.Priorities)
                .Where(s => s.Id == request.Id)
                .FirstOrDefaultAsync();

            scheme.Name = request.Name;
            scheme.Description = request.Description;
            scheme.Priorities.Clear();

            if (request.PriorityIds?.ToList().Count > 0)
            {
                var priorities = await _context.Priorities
                    .Where(p => request.PriorityIds.Contains(p.Id))
                    .ToListAsync();

                scheme.Priorities.AddRange(priorities.Select(p => new PrioritySchemePriority
                {
                    PriorityScheme = scheme,
                    Priority = p
                }));
            }
            else
            {
                var defaultPriority = await _context.Priorities
                    .Where(p => p.IsDefault).FirstAsync();

                scheme.Priorities.Add(new PrioritySchemePriority { Priority = defaultPriority, PriorityScheme = scheme });
            }

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}