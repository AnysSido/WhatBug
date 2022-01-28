using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PrioritySchemes.Commands.CreatePriorityScheme
{
    public class CreatePrioritySchemeCommandValidator : AbstractValidator<CreatePrioritySchemeCommand>
    {
        private IWhatBugDbContext _context;

        public CreatePrioritySchemeCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Priority scheme name cannot be empty")
                .MustAsync(NameUnique).WithMessage(cmd => $"A priority scheme with the name {cmd.Name} already exists");

            RuleFor(v => v.PriorityIds)
                .Cascade(CascadeMode.Stop)
                .MustAsync(AllExist).WithException(cmd => new RecordNotFoundException());
        }

        public async Task<bool> NameUnique(CreatePrioritySchemeCommand command, string name, CancellationToken cancellationToken)
        {
            return !await _context.PrioritySchemes.AnyAsync(p => p.Name == name);
        }

        public async Task<bool> AllExist(CreatePrioritySchemeCommand command, IEnumerable<int> priorityIds, CancellationToken cancellationToken)
        {
            if (command.PriorityIds == null || !command.PriorityIds.Any())
                return true;

            var priorities = await _context.Priorities.Where(p => command.PriorityIds.Contains(p.Id)).ToListAsync();
            return priorities.Count == command.PriorityIds.Count();
        }
    }
}