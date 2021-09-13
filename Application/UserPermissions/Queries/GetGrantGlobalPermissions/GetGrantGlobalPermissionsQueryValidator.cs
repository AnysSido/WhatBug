using FluentValidation;

namespace WhatBug.Application.UserPermissions.Queries.GetGrantGlobalPermissions
{
    public class GetGrantGlobalPermissionsQueryValidator : AbstractValidator<GetGrantGlobalPermissionsQuery>
    {
        public GetGrantGlobalPermissionsQueryValidator()
        {
            RuleFor(v => v.UserId).NotNull();
        }
    }
}
