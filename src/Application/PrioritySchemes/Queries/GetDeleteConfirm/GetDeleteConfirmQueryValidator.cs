using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PrioritySchemes.Queries.GetDeleteConfirm
{
    public class GetDeleteConfirmQueryValidator : AbstractValidator<GetDeleteConfirmQuery>
    {
        private IWhatBugDbContext _context;

        public GetDeleteConfirmQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.SchemeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.SchemeId)))
                .MustAsync(Exist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GetDeleteConfirmQuery query, int schemeId, CancellationToken cancellationToken)
        {
            return await _context.PrioritySchemes.AnyAsync(r => r.Id == schemeId);
        }
    }
}