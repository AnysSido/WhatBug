using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Projects.Queries.GetUsersAndRoles
{
    public class GetUsersAndRolesQueryValidator : AbstractValidator<GetUsersAndRolesQuery>
    {
        public GetUsersAndRolesQueryValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty();
        }
    }
}
