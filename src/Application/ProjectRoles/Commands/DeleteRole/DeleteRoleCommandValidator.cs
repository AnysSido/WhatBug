using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.ProjectRoles.Commands.DeleteRole
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        private IWhatBugDbContext _context;

        public DeleteRoleCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.RoleId)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.RoleId)))
                .MustAsync(Exist).WithException(cmd => new RecordNotFoundException());
        }

        public async Task<bool> Exist(DeleteRoleCommand command, int roleId, CancellationToken cancellationToken)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }
    }
}