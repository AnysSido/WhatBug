using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Issues.Commands.SetIssueStatus
{
    [Authorize(Permissions.SetIssueStatus)]
    public record SetIssueStatusCommand : ICommand<Response>, IRequireIssueAuthorization
    {
        public string IssueId { get; set; }
        public int IssueStatusId { get; set; }
    }

    public class SetIssueStatusCommandHandler : IRequestHandler<SetIssueStatusCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public SetIssueStatusCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(SetIssueStatusCommand request, CancellationToken cancellationToken)
        {
            var issue = await _context.Issues
                .Include(i => i.IssueStatus)
                .FirstOrDefaultAsync(i => i.Id == request.IssueId);

            var issueStatus = await _context.IssueStatuses
                .FirstOrDefaultAsync(s => s.Id == request.IssueStatusId);

            issue.IssueStatus = issueStatus;

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}
