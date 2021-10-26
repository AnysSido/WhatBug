using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.PrioritySchemes.Commands.DeletePriorityScheme
{
    [Authorize(Permissions.ManagePrioritySchemes)]
    public class DeletePrioritySchemeCommand : ICommand<Response>
    {
        public int SchemeId { get; init; }
    }

    public class DeletePrioritySchemeCommandHandler : IRequestHandler<DeletePrioritySchemeCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public DeletePrioritySchemeCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(DeletePrioritySchemeCommand request, CancellationToken cancellationToken)
        {
            var scheme = await _context.PrioritySchemes.FirstAsync(s => s.Id == request.SchemeId);
            var projectsUsingScheme = await _context.Projects.Where(p => p.PrioritySchemeId == request.SchemeId).ToListAsync();
            var defaultScheme = await _context.PrioritySchemes.FirstAsync(s => s.IsDefault);

            projectsUsingScheme.ForEach(p => p.PriorityScheme = defaultScheme);

            _context.PrioritySchemes.Remove(scheme);
            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}