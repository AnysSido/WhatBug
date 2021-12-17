using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Commands.SetIssueType
{
    internal class SetIssueTypeCommandValidator : AbstractValidator<SetIssueTypeCommand>
    {
        private IWhatBugDbContext _context;

        public SetIssueTypeCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.IssueId)
                .NotEmpty().WithException(cmd => new ArgumentException(nameof(cmd.IssueId)))
                .MustAsync(IssueExist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.IssueTypeId)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.IssueTypeId)))
                .MustAsync(IssueTypeExist).WithException(cmd => new RecordNotFoundException());
        }

        public async Task<bool> IssueExist(SetIssueTypeCommand command, string issueId, CancellationToken cancellationToken)
        {
            return await _context.Issues.AnyAsync(i => i.Id == issueId);
        }

        public async Task<bool> IssueTypeExist(SetIssueTypeCommand command, int issueTypeId, CancellationToken cancellationToken)
        {
            return await _context.IssueTypes.AnyAsync(i => i.Id == issueTypeId);
        }
    }
}
