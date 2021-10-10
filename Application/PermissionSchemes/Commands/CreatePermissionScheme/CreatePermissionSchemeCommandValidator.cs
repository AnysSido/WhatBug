using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme
{
    public class CreatePermissionSchemeCommandValidator : AbstractValidator<CreatePermissionSchemeCommand>
    {
        private IWhatBugDbContext _context;

        public CreatePermissionSchemeCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Scheme name cannot be empty")
                .MustAsync(HaveUniqueName).WithMessage(cmd => $"A permission scheme with the name {cmd.Name} already exists");
        }

        public async Task<bool> HaveUniqueName(CreatePermissionSchemeCommand command, string name, CancellationToken cancellationToken)
        {
            return !await _context.PermissionSchemes.AnyAsync(r => r.Name == name);
        }
    }
}
