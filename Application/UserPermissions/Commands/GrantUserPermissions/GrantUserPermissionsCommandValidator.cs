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
using WhatBug.Application.UserPermissions.Commands.GrantUserPermissions;

namespace WhatBug.Application.UserPermissions.Commands.GrantGlobalPermissions
{
    public class GrantUserPermissionsCommandValidator : AbstractValidator<GrantUserPermissionsCommand>
    {
        private IWhatBugDbContext _context;

        public GrantUserPermissionsCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.UserId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.UserId)))
                .MustAsync(Exist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.PermissionIds)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithException(cmd => new ArgumentException(nameof(cmd.PermissionIds)))
                .MustAsync(AllExist).WithException(cmd => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GrantUserPermissionsCommand command, int userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(r => r.Id == userId);
        }

        public async Task<bool> AllExist(GrantUserPermissionsCommand command, IEnumerable<int> permissionIds, CancellationToken cancellationToken)
        {
            var permissions = await _context.Permissions.Where(p => command.PermissionIds.Contains(p.Id)).ToListAsync();
            return permissions.Count == command.PermissionIds.Count();
        }
    }
}
