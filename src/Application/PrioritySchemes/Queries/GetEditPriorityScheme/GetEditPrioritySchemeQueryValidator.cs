using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme
{
    public class GetEditPrioritySchemeQueryValidator : AbstractValidator<GetEditPrioritySchemeQuery>
    {
        private IWhatBugDbContext _context;

        public GetEditPrioritySchemeQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.Id)))
                .MustAsync(Exist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GetEditPrioritySchemeQuery query, int prioritySchemeId, CancellationToken cancellationToken)
        {
            return await _context.PrioritySchemes.AnyAsync(r => r.Id == prioritySchemeId);
        }
    }
}