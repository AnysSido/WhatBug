using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions
{
    public class GetGrantRolePermissionsQueryValidator : AbstractValidator<GetGrantRolePermissionsQuery>
    {
        public GetGrantRolePermissionsQueryValidator()
        {
            RuleFor(v => v.SchemeId).NotEmpty();
            RuleFor(v => v.RoleId).NotEmpty();
        }
    }
}
