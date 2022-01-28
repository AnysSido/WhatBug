using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Commands.CreateIssue
{
    [Authorize(Permissions.CreateIssue)]
    public record CreateIssueCommand : ICommand<Response>, IRequireProjectAuthorization
    {
        public int ProjectId { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public int IssueTypeId { get; set; }
        public int AssigneeId { get; set; }
        public int ReporterId { get; set; }
    }

    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public CreateIssueCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId);
            var priority = await _context.Priorities.FirstOrDefaultAsync(p => p.Id == request.PriorityId);
            var issueType = await _context.IssueTypes.FirstOrDefaultAsync(i => i.Id == request.IssueTypeId);
            var reporter = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.ReporterId);
            var assignee = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.AssigneeId);
            var issueCounter = project.IssueCounter + 1;

            var issue = new Issue
            {
                Id = project.Key + '-' + issueCounter,
                Summary = request.Summary,
                Description = request.Description,
                Project = project,
                Priority = priority,
                IssueType = issueType,
                Reporter = reporter,
                Assignee = assignee,
                IssueStatus = await _context.IssueStatuses.FirstAsync(s => s.Id == IssueStatuses.Backlog.Id)
            };

            project.IssueCounter = issueCounter;
            _context.Issues.Add(issue);

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}
