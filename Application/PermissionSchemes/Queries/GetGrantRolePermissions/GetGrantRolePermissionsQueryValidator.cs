using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions
{
    public class GetGrantRolePermissionsQueryValidator : AbstractValidator<GetGrantRolePermissionsQuery>
    {
        private IWhatBugDbContext _context;

        public GetGrantRolePermissionsQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.SchemeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.SchemeId)))
                .MustAsync(SchemeExist).WithException(query => new RecordNotFoundException());

            RuleFor(v => v.RoleId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.SchemeId)))
                .MustAsync(RoleExist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> SchemeExist(GetGrantRolePermissionsQuery query, int schemeId, CancellationToken cancellationToken)
        {
            return await _context.PermissionSchemes.AnyAsync(r => r.Id == schemeId);
        }

        public async Task<bool> RoleExist(GetGrantRolePermissionsQuery query, int roleId, CancellationToken cancellationToken)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }
    }
}