using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PermissionSchemes.Commands.EditPermissionScheme
{
    public class EditPermissionSchemeCommandValidator : AbstractValidator<EditPermissionSchemeCommand>
    {
        private IWhatBugDbContext _context;

        public EditPermissionSchemeCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.SchemeId)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.SchemeId)))
                .MustAsync(Exist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Scheme name cannot be empty")
                .MustAsync(BeUnique).WithMessage(cmd => $"A scheme with the name {cmd.Name} already exists");
        }

        public async Task<bool> BeUnique(EditPermissionSchemeCommand command, string name, CancellationToken cancellationToken)
        {
            return !await _context.PermissionSchemes.AnyAsync(r => r.Id != command.SchemeId && r.Name == name);
        }

        public async Task<bool> Exist(EditPermissionSchemeCommand command, int schemeId, CancellationToken cancellationToken)
        {
            return await _context.PermissionSchemes.AnyAsync(r => r.Id == schemeId);
        }
    }
}