using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Result;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, Result>
    {
        private readonly IWhatBugDbContext _context;

        public CreateIssueCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId);
            if (project == null)
                return Result.Failure(Errors.Issues.ProjectNotFound(request.ProjectId));

            var priority = await _context.Priorities.FirstOrDefaultAsync(p => p.Id == request.PriorityId);
            if (priority == null)
                return Result.Failure(Errors.Issues.PriorityNotFound(request.PriorityId));

            var issueType = await _context.IssueTypes.FirstOrDefaultAsync(i => i.Id == request.IssueTypeId);
            if (issueType == null)
                return Result.Failure(Errors.Issues.IssueTypeNotFound(request.IssueTypeId));

            var reporter = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.ReporterId);
            if (reporter == null)
                return Result.Failure(Errors.Issues.ReporterNotFound(request.ReporterId));

            User assignee = null;
            if (request.AssigneeId != null)
            {
                assignee = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.AssigneeId);
                if (assignee == null)
                    return Result.Failure(Errors.Issues.AssigneeNotFound((int)request.AssigneeId));
            }

            var issueCounter = project.IssueCounter + 1;

            // TODO: Check permissions
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
                IssueStatus = await _context.IssueStatuses.FirstAsync(s => s.Name ==  "Backlog") // TODO: Remove magic string
            };

            project.IssueCounter = issueCounter;
            _context.Issues.Add(issue);

            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
