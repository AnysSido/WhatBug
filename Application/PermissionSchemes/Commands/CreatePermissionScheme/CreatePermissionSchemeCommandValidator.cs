using FluentValidation;

namespace WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme
{
    public class CreatePermissionSchemeCommandValidator : AbstractValidator<CreatePermissionSchemeCommand>
    {
        public CreatePermissionSchemeCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("Scheme name cannot be empty");
        }
    }
}
