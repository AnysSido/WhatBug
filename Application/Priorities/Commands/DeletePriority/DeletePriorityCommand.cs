using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Priorities.Commands.DeletePriority
{
    [Authorize(Permissions.ManagePriorities)]
    public record DeletePriorityCommand : ICommand<Response>
    {
        public int PriorityId { get; init; }
    }

    public class DeletePriorityCommandHandler : IRequestHandler<DeletePriorityCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public DeletePriorityCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(DeletePriorityCommand request, CancellationToken cancellationToken)
        {
            var priority = await _context.Priorities.FirstAsync(p => p.Id == request.PriorityId);
            var issuesUsingPriority = await _context.Issues.Where(i => i.PriorityId == request.PriorityId).ToListAsync();
            var defaultPriority = await _context.Priorities.FirstAsync(p => p.IsDefault);

            issuesUsingPriority.ForEach(i => i.Priority = defaultPriority);

            _context.Priorities.Remove(priority);
            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}