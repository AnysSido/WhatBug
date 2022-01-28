using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Commands.GrantRolePermissions
{
    public class GrantRolePermissionsCommandValidator : AbstractValidator<GrantRolePermissionsCommand>
    {
        private IWhatBugDbContext _context;

        public GrantRolePermissionsCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.SchemeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.SchemeId)))
                .MustAsync(Exist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.RoleId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.RoleId)))
                .MustAsync(RoleExist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.PermissionIds)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithException(cmd => new ArgumentException(nameof(cmd.PermissionIds)))
                .MustAsync(AllExist).WithException(cmd => new RecordNotFoundException())
                .MustAsync(BeCorrectPermissionType).WithException(cmd => new ArgumentException(nameof(cmd.PermissionIds)));
        }

        public async Task<bool> Exist(GrantRolePermissionsCommand command, int schemeId, CancellationToken cancellationToken)
        {
            return await _context.PermissionSchemes.AnyAsync(r => r.Id == schemeId);
        }

        public async Task<bool> AllExist(GrantRolePermissionsCommand command, IEnumerable<int> permissionIds, CancellationToken cancellationToken)
        {
            var permissions = await _context.Permissions.Where(p => command.PermissionIds.Contains(p.Id)).ToListAsync();
            return permissions.Count == command.PermissionIds.Count();
        }

        public async Task<bool> BeCorrectPermissionType(GrantRolePermissionsCommand command, IEnumerable<int> permissionIds, CancellationToken cancellationToken)
        {
            var permissions = await _context.Permissions.Where(p => command.PermissionIds.Contains(p.Id)).ToListAsync();
            return !permissions.Where(p => p.Type != PermissionType.Project).Any();
        }

        public async Task<bool> RoleExist(GrantRolePermissionsCommand command, int roleId, CancellationToken cancellationToken)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }
    }
}
