using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.ProjectRoles.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        private IWhatBugDbContext _context;

        public CreateRoleCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name).NotEmpty().WithMessage("Role name cannot be empty")
                .MustAsync(HaveUniqueName).WithMessage(cmd => $"A role with the name {cmd.Name} already exists");
        }

        public async Task<bool> HaveUniqueName(CreateRoleCommand command, string name, CancellationToken cancellationToken)
        {
            return !await _context.Roles.AnyAsync(r => r.Name == name);
        }
    }
}