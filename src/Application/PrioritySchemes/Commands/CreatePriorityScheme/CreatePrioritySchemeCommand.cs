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

namespace WhatBug.Application.PrioritySchemes.Commands.CreatePriorityScheme
{
    [Authorize(Permissions.ManagePrioritySchemes)]
    public record CreatePrioritySchemeCommand : ICommand<Response<int>>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public IEnumerable<int> PriorityIds { get; init; }
    }

    public class CreatePrioritySchemeCommandHandler : IRequestHandler<CreatePrioritySchemeCommand, Response<int>>
    {
        private readonly IWhatBugDbContext _context;

        public CreatePrioritySchemeCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(CreatePrioritySchemeCommand request, CancellationToken cancellationToken)
        {
            var scheme = new PriorityScheme
            {
                Name = request.Name,
                Description = request.Description,
                Priorities = new List<PrioritySchemePriority>()
            };

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

            _context.PrioritySchemes.Add(scheme);
            await _context.SaveChangesAsync();

            return Response<int>.Success(scheme.Id);
        }
    }
}