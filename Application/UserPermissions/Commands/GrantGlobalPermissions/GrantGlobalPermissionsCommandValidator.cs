﻿using FluentValidation;

namespace WhatBug.Application.UserPermissions.Commands.GrantGlobalPermissions
{
    public class GrantGlobalPermissionsCommandValidator : AbstractValidator<GrantGlobalPermissionsCommand>
    {
        public GrantGlobalPermissionsCommandValidator()
        {
            RuleFor(v => v.UserId).NotEmpty();
            RuleFor(v => v.PermissionIds).NotNull();
        }
    }
}