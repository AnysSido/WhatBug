using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.PermissionSchemes.Commands.GrantRolePermissions
{
    public class GrantRolePermissionsCommandValidator : AbstractValidator<GrantRolePermissionsCommand>
    {
        public GrantRolePermissionsCommandValidator()
        {
            RuleFor(v => v.SchemeId).NotEmpty();
            RuleFor(v => v.RoleId).NotEmpty();
            RuleFor(v => v.PermissionIds).NotNull();
        }
    }
}
