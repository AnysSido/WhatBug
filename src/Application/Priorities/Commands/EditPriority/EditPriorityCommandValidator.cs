using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Priorities.Commands.EditPriority
{
    public class EditPriorityCommandValidator : AbstractValidator<EditPriorityCommand>
    {
        private IWhatBugDbContext _context;

        public EditPriorityCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.Id)))
                .MustAsync(IdExist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Priority name cannot be empty")
                .MustAsync(NameUnique).WithMessage(cmd => $"A priority with the name {cmd.Name} already exists");

            RuleFor(v => v.ColorId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.ColorId)))
                .MustAsync(ColorExist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.IconId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.IconId)))
                .MustAsync(IconExist).WithException(cmd => new RecordNotFoundException());
        }

        public async Task<bool> IdExist(EditPriorityCommand command, int id, CancellationToken cancellationToken)
        {
            return await _context.Priorities.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> NameUnique(EditPriorityCommand command, string name, CancellationToken cancellationToken)
        {
            return !await _context.Priorities.AnyAsync(p => p.Id != command.Id && p.Name == name);
        }

        public async Task<bool> ColorExist(EditPriorityCommand command, int colorId, CancellationToken cancellationToken)
        {
            return await _context.Colors.AnyAsync(c => c.Id == colorId);
        }

        public async Task<bool> IconExist(EditPriorityCommand command, int iconId, CancellationToken cancellationToken)
        {
            return await _context.Icons.AnyAsync(i => i.Id == iconId);
        }
    }
}