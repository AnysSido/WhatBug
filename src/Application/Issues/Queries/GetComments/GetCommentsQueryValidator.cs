using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Queries.GetComments
{
    public class GetCommentsQueryValidator : AbstractValidator<GetCommentsQuery>
    {
        private readonly IWhatBugDbContext _context;

        public GetCommentsQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.IssueId)
                .NotEmpty().WithException(query => new ArgumentException(nameof(query.IssueId)))
                .MustAsync(Exist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GetCommentsQuery command, string issueId, CancellationToken cancellationToken)
        {
            return await _context.Issues.AnyAsync(p => p.Id == issueId);
        }
    }
}
