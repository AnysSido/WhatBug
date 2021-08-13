using FluentValidation;

namespace WhatBug.Application.Projects.Queries.GetAssignUsersToRole
{
    public class GetAssignUsersToRoleQueryValidator : AbstractValidator<GetAssignUsersToRoleQuery>
    {
        public GetAssignUsersToRoleQueryValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty();
        }
    }
}
