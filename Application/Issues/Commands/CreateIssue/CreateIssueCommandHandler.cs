using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand>
    {
        private readonly IWhatBugDbContext _context;

        public CreateIssueCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId);

            if (project == null)
            {
                // TODO: Throw not found exception
            }

            var issueCounter = project.IssueCounter + 1;


            // TODO: Check permissions
            var issue = new Issue
            {
                Id = project.Key + '-' + issueCounter,
                Summary = request.Summary,
                Description = request.Description,
                ProjectId = request.ProjectId,
                PriorityId = request.PriorityId,
                IssueTypeId = request.IssueTypeId,
                ReporterId = request.ReporterId,
                AssigneeId = request.AssigneeId,
                IssueStatus = await _context.IssueStatuses.FirstAsync(s => s.Name ==  "Backlog") // TODO: Remove magic string
            };

            project.IssueCounter = issueCounter;
            _context.Issues.Add(issue);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
