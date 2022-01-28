using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Issues.Commands.SetIssueSummary
{
    [Authorize(Permissions.EditIssue)]
    public record SetIssueSummaryCommand : ICommand<Response>, IRequireIssueAuthorization
    {
        public string IssueId { get; init; }
        public string Summary { get; init; }
    }

    public class SetIssueSummaryCommandHandler : IRequestHandler<SetIssueSummaryCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public SetIssueSummaryCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(SetIssueSummaryCommand request, CancellationToken cancellationToken)
        {
            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == request.IssueId);

            issue.Summary = request.Summary;

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}
