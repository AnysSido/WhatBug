using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Priorities.Commands.ReorderPriorities
{
    [Authorize(Permissions.ManagePriorities)]
    public record ReorderPrioritiesCommand : ICommand<Response>
    {
        public IList<int> Ids { get; set; }
    }

    public class ReorderPrioritiesCommandHandler : IRequestHandler<ReorderPrioritiesCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public ReorderPrioritiesCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(ReorderPrioritiesCommand request, CancellationToken cancellationToken)
        {
            var priorities = await _context.Priorities.ToListAsync();

            priorities.ForEach(p => p.Order = request.Ids.IndexOf(p.Id));

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}