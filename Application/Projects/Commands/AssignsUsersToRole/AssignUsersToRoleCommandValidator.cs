using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Projects.Commands.AssignsUsersToRole
{
    public class AssignUsersToRoleCommandValidator : AbstractValidator<AssignUsersToRoleCommand>
    {
        public AssignUsersToRoleCommandValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty();
            RuleFor(v => v.RoleId).NotEmpty();
            RuleFor(v => v.UserIds).NotEmpty();
        }
    }
}
