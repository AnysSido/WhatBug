using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Issues.Commands.SetIssuePriority
{
    [Authorize(Permissions.EditIssue)]
    public record SetIssuePriorityCommand : ICommand<Response>, IRequireIssueAuthorization
    {
        public string IssueId { get; set; }
        public int PriorityId { get; set; }
    }

    public class SetIssuePriorityCommandHandler : IRequestHandler<SetIssuePriorityCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public SetIssuePriorityCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(SetIssuePriorityCommand request, CancellationToken cancellationToken)
        {
            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == request.IssueId);

            issue.PriorityId = request.PriorityId;

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}
