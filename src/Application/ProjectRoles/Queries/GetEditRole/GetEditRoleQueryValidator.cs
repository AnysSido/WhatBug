using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.ProjectRoles.Queries.GetEditRole
{
    public class GetEditRoleQueryValidator : AbstractValidator<GetEditRoleQuery>
    {
        private IWhatBugDbContext _context;

        public GetEditRoleQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.RoleId)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.RoleId)))
                .MustAsync(Exist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GetEditRoleQuery query, int roleId, CancellationToken cancellationToken)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }
    }
}