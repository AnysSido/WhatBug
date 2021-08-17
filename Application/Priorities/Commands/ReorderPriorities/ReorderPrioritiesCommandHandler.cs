using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Priorities.Commands.ReorderPriorities
{
    public class ReorderPrioritiesCommandHandler : IRequestHandler<ReorderPrioritiesCommand>
    {
        private readonly IWhatBugDbContext _context;

        public ReorderPrioritiesCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ReorderPrioritiesCommand request, CancellationToken cancellationToken)
        {
            var priorities = await _context.Priorities.ToListAsync();
            
            if (priorities.Count != request.Ids.Count)
            {
                // TODO: Throw exception
            }

            priorities.ForEach(p => p.Order = request.Ids.IndexOf(p.Id));

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
