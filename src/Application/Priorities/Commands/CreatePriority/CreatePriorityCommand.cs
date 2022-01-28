using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Priorities.Commands.CreatePriority
{
    [Authorize(Permissions.ManagePriorities)]
    public record CreatePriorityCommand : ICommand<Response<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColorId { get; set; }
        public int IconId { get; set; }
    }

    public class CreatePriorityCommandHandler : IRequestHandler<CreatePriorityCommand, Response<int>>
    {
        private readonly IWhatBugDbContext _context;

        public CreatePriorityCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(CreatePriorityCommand request, CancellationToken cancellationToken)
        {
            var color = await _context.Colors.FirstAsync(c => c.Id == request.ColorId);
            var icon = await _context.Icons.FirstAsync(i => i.Id == request.IconId);
            var order = await _context.Priorities.MaxAsync(p => (int?)p.Order) ?? 0;

            var priority = new Priority
            {
                Name = request.Name,
                Description = !string.IsNullOrEmpty(request.Description) ? request.Description : null,
                Order = order + 1,
                Color = color,
                Icon = icon
            };

            _context.Priorities.Add(priority);
            await _context.SaveChangesAsync();

            return Response<int>.Success(priority.Id);
        }
    }
}