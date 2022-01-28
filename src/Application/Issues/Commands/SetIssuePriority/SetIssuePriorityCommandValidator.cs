using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Commands.SetIssuePriority
{
    internal class SetIssuePriorityCommandValidator : AbstractValidator<SetIssuePriorityCommand>
    {
        private IWhatBugDbContext _context;

        public SetIssuePriorityCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.IssueId)
                .NotEmpty().WithException(cmd => new ArgumentException(nameof(cmd.IssueId)))
                .MustAsync(IssueExist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.PriorityId)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.PriorityId)))
                .MustAsync(PriorityExist).WithException(cmd => new RecordNotFoundException());
        }

        public async Task<bool> IssueExist(SetIssuePriorityCommand command, string issueId, CancellationToken cancellationToken)
        {
            return await _context.Issues.AnyAsync(i => i.Id == issueId);
        }

        public async Task<bool> PriorityExist(SetIssuePriorityCommand command, int priorityId, CancellationToken cancellationToken)
        {
            return await _context.Priorities.AnyAsync(p => p.Id == priorityId);
        }
    }
}
