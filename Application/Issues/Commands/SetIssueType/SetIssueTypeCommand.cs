using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Issues.Commands.SetIssueType
{
    [Authorize(Permissions.EditIssue)]
    public record SetIssueTypeCommand : ICommand<Response>, IRequireIssueAuthorization
    {
        public string IssueId { get; init; }
        public int IssueTypeId { get; init; }
    }

    public class SetIssueTypeCommandHandler : IRequestHandler<SetIssueTypeCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public SetIssueTypeCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(SetIssueTypeCommand request, CancellationToken cancellationToken)
        {
            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == request.IssueId);

            issue.IssueTypeId = request.IssueTypeId;

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}
