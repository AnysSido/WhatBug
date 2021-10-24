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

namespace WhatBug.Application.PrioritySchemes.Commands.EditPriorityScheme
{
    public class EditPrioritySchemeCommandValidator : AbstractValidator<EditPrioritySchemeCommand>
    {
        private IWhatBugDbContext _context;

        public EditPrioritySchemeCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.Id)))
                .MustAsync(IdExist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Priority scheme name cannot be empty")
                .MustAsync(NameUnique).WithMessage(cmd => $"A priority scheme with the name {cmd.Name} already exists");

            RuleFor(v => v.PriorityIds)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithException(cmd => new ArgumentException(nameof(cmd.PriorityIds)))
                .MustAsync(AllExist).WithException(cmd => new RecordNotFoundException());
        }

        public async Task<bool> IdExist(EditPrioritySchemeCommand command, int id, CancellationToken cancellationToken)
        {
            return await _context.PrioritySchemes.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> NameUnique(EditPrioritySchemeCommand command, string name, CancellationToken cancellationToken)
        {
            return !await _context.PrioritySchemes.AnyAsync(p => p.Id != command.Id && p.Name == name);
        }

        public async Task<bool> AllExist(EditPrioritySchemeCommand command, IEnumerable<int> priorityIds, CancellationToken cancellationToken)
        {
            var priorities = await _context.Priorities.Where(p => command.PriorityIds.Contains(p.Id)).ToListAsync();
            return priorities.Count == command.PriorityIds.Count();
        }
    }
}