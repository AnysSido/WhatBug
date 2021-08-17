using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IWhatBugDbContext _context;

        public CreateProjectCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions

            var existingProjectKey = _context.Projects.FirstOrDefaultAsync(p => p.Key == request.Key);

            if (existingProjectKey != null)
            {
                // TODO: Throw key exists exception
            }

            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                Key = request.Key,
                PrioritySchemeId = request.PrioritySchemeId,
                IssueCounter = 0
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
