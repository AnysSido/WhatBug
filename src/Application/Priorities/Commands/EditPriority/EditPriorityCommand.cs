using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Priorities.Commands.EditPriority
{
    [Authorize(Permissions.ManagePriorities)]
    public record EditPriorityCommand : ICommand<Response>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColorId { get; set; }
        public int IconId { get; set; }
    }

    public class EditPriorityCommandHandler : IRequestHandler<EditPriorityCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public EditPriorityCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(EditPriorityCommand request, CancellationToken cancellationToken)
        {
            var priority = await _context.Priorities
                .Include(p => p.Color)
                .Include(p => p.Icon)
                .FirstAsync(p => p.Id == request.Id);

            if (!priority.IsDefault)
                priority.Name = request.Name;

            priority.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : null;
            priority.Color = await _context.Colors.FirstAsync(c => c.Id == request.ColorId);
            priority.Icon = await _context.Icons.FirstAsync(i => i.Id == request.IconId);

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}