using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Priorities.Commands.ReorderPriorities
{
    public class ReorderPrioritiesCommandValidator : AbstractValidator<ReorderPrioritiesCommand>
    {
        private IWhatBugDbContext _context;

        public ReorderPrioritiesCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.Ids)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithException(cmd => new ArgumentException(nameof(cmd.Ids)))
                .MustAsync(ExistAndBeUnique).WithException(cmd => new ArgumentException(nameof(cmd.Ids)));
        }

        public async Task<bool> ExistAndBeUnique(ReorderPrioritiesCommand command, IList<int> ids, CancellationToken cancellationToken)
        {
            var priorities = await _context.Priorities.Where(p => command.Ids.Contains(p.Id)).ToListAsync();
            return priorities.Distinct().Count() == ids.Count;
        }
    }
}