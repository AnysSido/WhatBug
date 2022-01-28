using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Issues.Commands.SetIssueDescription
{
    [Authorize(Permissions.EditIssue)]
    public record SetIssueDescriptionCommand : ICommand<Response>, IRequireIssueAuthorization
    {
        public string IssueId { get; init; }
        public string Description { get; init; }
    }

    public class SetIssueDescriptionCommandHandler : IRequestHandler<SetIssueDescriptionCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public SetIssueDescriptionCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(SetIssueDescriptionCommand request, CancellationToken cancellationToken)
        {
            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == request.IssueId);

            issue.Description = request.Description;
            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}
