using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.ProjectRoles.Commands.EditRole
{
    public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
    {
        private IWhatBugDbContext _context;

        public EditRoleCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.RoleId)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.RoleId)))
                .MustAsync(Exist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Role name cannot be empty")
                .MustAsync(BeUnique).WithMessage(cmd => $"A role with the name {cmd.Name} already exists");
        }

        public async Task<bool> BeUnique(EditRoleCommand command, string name, CancellationToken cancellationToken)
        {
            return !await _context.Roles.AnyAsync(r => r.Id != command.RoleId && r.Name == name);
        }

        public async Task<bool> Exist(EditRoleCommand command, int roleId, CancellationToken cancellationToken)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }
    }
}