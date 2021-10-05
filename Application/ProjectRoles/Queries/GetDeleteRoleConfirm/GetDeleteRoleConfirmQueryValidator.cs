using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.ProjectRoles.Queries.GetDeleteRoleConfirm
{
    public class GetDeleteRoleConfirmQueryValidator : AbstractValidator<GetDeleteRoleConfirmQuery>
    {
        private IWhatBugDbContext _context;

        public GetDeleteRoleConfirmQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.RoleId)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.RoleId)))
                .MustAsync(Exist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GetDeleteRoleConfirmQuery query, int roleId, CancellationToken cancellationToken)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }
    }
}