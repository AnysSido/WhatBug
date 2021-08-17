using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Commands.SetIssueStatus
{
    public class SetIssueStatusCommandHandler : IRequestHandler<SetIssueStatusCommand>
    {
        private readonly IWhatBugDbContext _context;

        public SetIssueStatusCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SetIssueStatusCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var issue = _context.Issues.Include(i => i.IssueStatus).FirstOrDefault(i => i.Id == request.IssueId);

            if (issue == null)
            {
                // TODO: Throw not found
            }

            var issueStatus = _context.IssueStatuses.FirstOrDefault(s => s.Id == request.IssueStatusId);

            if (issueStatus == null)
            {
                // TODO: Throw not found
            }

            issue.IssueStatus = issueStatus;
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
