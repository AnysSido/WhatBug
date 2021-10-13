using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PermissionSchemes.Queries.GetRolesAndPermissions
{
    public class GetRolesAndPermissionsQueryValidator : AbstractValidator<GetRolesAndPermissionsQuery>
    {
        private IWhatBugDbContext _context;

        public GetRolesAndPermissionsQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.SchemeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.SchemeId)))
                .MustAsync(Exist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GetRolesAndPermissionsQuery query, int schemeId, CancellationToken cancellationToken)
        {
            return await _context.PermissionSchemes.AnyAsync(r => r.Id == schemeId);
        }
    }
}