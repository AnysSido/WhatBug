using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Commands.SetIssueDescription
{
    public class SetIssueDescriptionCommandHandler : IRequestHandler<SetIssueDescriptionCommand>
    {
        private readonly IWhatBugDbContext _context;

        public SetIssueDescriptionCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SetIssueDescriptionCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == request.IssueId);

            if (issue == null)
            {
                // TODO: Throw found exception
            }

            issue.Description = request.Description;
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
