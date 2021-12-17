using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Queries.GetAttachments
{
    public class GetAttachmentsQueryValidator : AbstractValidator<GetAttachmentsQuery>
    {
        private IWhatBugDbContext _context;

        public GetAttachmentsQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.IssueId)
                .NotEmpty().WithException(cmd => new ArgumentException(nameof(cmd.IssueId)))
                .MustAsync(IssueExist).WithException(cmd => new RecordNotFoundException());
        }

        public async Task<bool> IssueExist(GetAttachmentsQuery command, string issueId, CancellationToken cancellationToken)
        {
            return await _context.Issues.AnyAsync(i => i.Id == issueId);
        }
    }
}
