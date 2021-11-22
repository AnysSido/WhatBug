using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Commands.SetIssueStatus
{
    public class SetIssueStatusCommandValidator : AbstractValidator<SetIssueStatusCommand>
    {
        private IWhatBugDbContext _context;

        public SetIssueStatusCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.IssueId)
                .NotEmpty().WithException(cmd => new ArgumentException(nameof(cmd.IssueId)))
                .MustAsync(IssueExist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.IssueStatusId)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.IssueStatusId)))
                .MustAsync(StatusExist).WithException(cmd => new RecordNotFoundException());
        }

        public async Task<bool> IssueExist(SetIssueStatusCommand command, string issueId, CancellationToken cancellationToken)
        {
            return await _context.Issues.AnyAsync(p => p.Id == issueId);
        }

        public async Task<bool> StatusExist(SetIssueStatusCommand command, int issueStatusId, CancellationToken cancellationToken)
        {
            return await _context.IssueStatuses.AnyAsync(p => p.Id == issueStatusId);
        }
    }
}
