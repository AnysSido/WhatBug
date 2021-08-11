using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Priorities.Commands.EditPriority
{
    public class EditPriorityCommandHandler : IRequestHandler<EditPriorityCommand>
    {
        private readonly IWhatBugDbContext _context;

        public EditPriorityCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(EditPriorityCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var priority = await _context.Priorities.Include(p => p.ColorIcon).SingleOrDefaultAsync(p => p.Id == request.Id);

            if (priority == null)
            {
                // TODO: Throw not found exception
            }

            priority.Name = request.Name;
            priority.Description = request.Description;
            priority.ColorIcon.Color = await _context.Colors.SingleAsync(c => c.Id == request.ColorId);
            priority.ColorIcon.Icon = await _context.Icons.SingleAsync(i => i.Id == request.IconId);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
