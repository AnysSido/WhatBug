using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Priorities.Commands.CreatePriority
{
    public class CreatePriorityCommandValidator : AbstractValidator<CreatePriorityCommand>
    {
        private IWhatBugDbContext _context;

        public CreatePriorityCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Priority name cannot be empty")
                .MustAsync(HaveUniqueName).WithMessage(cmd => $"A priority with the name {cmd.Name} already exists");

            RuleFor(v => v.ColorId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.ColorId)))
                .MustAsync(ColorExist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.IconId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.IconId)))
                .MustAsync(IconExist).WithException(cmd => new RecordNotFoundException());
        }

        public async Task<bool> HaveUniqueName(CreatePriorityCommand command, string name, CancellationToken cancellationToken)
        {
            return !await _context.Priorities.AnyAsync(r => r.Name == name);
        }

        public async Task<bool> ColorExist(CreatePriorityCommand command, int colorId, CancellationToken cancellationToken)
        {
            return await _context.Colors.AnyAsync(c => c.Id == colorId);
        }

        public async Task<bool> IconExist(CreatePriorityCommand command, int iconId, CancellationToken cancellationToken)
        {
            return await _context.Icons.AnyAsync(i => i.Id == iconId);
        }
    }
}