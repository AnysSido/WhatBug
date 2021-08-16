using FluentValidation;

namespace WhatBug.Application.Navigation.Queries.GetProjectNavigation
{
    public class GetProjectNavigationValidator : AbstractValidator<GetProjectNavigationQuery>
    {
        public GetProjectNavigationValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty();
        }
    }
}
