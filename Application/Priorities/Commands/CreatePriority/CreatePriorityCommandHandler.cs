using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Priorities.Commands.CreatePriority
{
    public class CreatePriorityCommandHandler : IRequestHandler<CreatePriorityCommand>
    {
        private readonly IWhatBugDbContext _context;

        public CreatePriorityCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreatePriorityCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var color = await _context.Colors.SingleAsync(c => c.Id == request.ColorId);
            var icon = await _context.Icons.SingleAsync(i => i.Id == request.IconId);            
            var order = await _context.Priorities.DefaultIfEmpty()
                .MaxAsync(p => (int?)p.Order) ?? 1; // EFCore throws error if MaxAsync returns empty collection on non-nullable type

            var priority = new Priority
            {
                Name = request.Name,
                Description = request.Description,
                Order = order + 1,
                Color = color,
                Icon = icon
            };
            _context.Priorities.Add(priority);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
