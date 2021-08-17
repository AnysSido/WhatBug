using FluentValidation;

namespace WhatBug.Application.PermissionSchemes.Queries.GetSchemeRoles
{
    public class GetSchemeRolesQueryValidator : AbstractValidator<GetSchemeRolesQuery>
    {
        public GetSchemeRolesQueryValidator()
        {
            RuleFor(v => v.SchemeId).NotEmpty();
        }
    }
}
