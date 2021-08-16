using FluentValidation;

namespace WhatBug.Application.Users.Queries.GetUserPermissions
{
    public class GetUserPermissionsQueryValidator : AbstractValidator<GetUserPermissionsQuery>
    {
        public GetUserPermissionsQueryValidator()
        {
            RuleFor(v => v.UserId).NotEmpty();
        }
    }
}
