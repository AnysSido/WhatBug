using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Commands.SetIssueSummary
{
    public class SetIssueSummaryCommandValidator : AbstractValidator<SetIssueSummaryCommand>
    {
        private IWhatBugDbContext _context;

        public SetIssueSummaryCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.IssueId)
                .NotEmpty().WithException(cmd => new ArgumentException(nameof(cmd.IssueId)))
                .MustAsync(Exist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.Summary)
                .NotEmpty().WithException(cmd => new ArgumentException(nameof(cmd.Summary)));
        }

        public async Task<bool> Exist(SetIssueSummaryCommand command, string issueId, CancellationToken cancellationToken)
        {
            return await _context.Issues.AnyAsync(i => i.Id == issueId);
        }
    }
}
