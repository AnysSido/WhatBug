using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Priorities.Queries.GetDeleteConfirm
{
    public class GetDeleteConfirmQueryValidator : AbstractValidator<GetDeleteConfirmQuery>
    {
        private IWhatBugDbContext _context;

        public GetDeleteConfirmQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.PriorityId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.PriorityId)))
                .MustAsync(Exist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GetDeleteConfirmQuery query, int priorityId, CancellationToken cancellationToken)
        {
            return await _context.Priorities.AnyAsync(r => r.Id == priorityId);
        }
    }
}