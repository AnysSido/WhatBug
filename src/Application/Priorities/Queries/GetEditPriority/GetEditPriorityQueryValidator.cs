using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Priorities.Queries.GetEditPriority
{
    public class GetEditPriorityQueryValidator : AbstractValidator<GetEditPriorityQuery>
    {
        private IWhatBugDbContext _context;

        public GetEditPriorityQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.Id)))
                .MustAsync(Exist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GetEditPriorityQuery query, int priorityId, CancellationToken cancellationToken)
        {
            return await _context.Priorities.AnyAsync(r => r.Id == priorityId);
        }
    }
}