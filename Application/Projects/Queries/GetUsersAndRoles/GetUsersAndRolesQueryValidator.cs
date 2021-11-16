using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Projects.Queries.GetUsersAndRoles
{
    public class GetUsersAndRolesQueryValidator : AbstractValidator<GetUsersAndRolesQuery>
    {
        private IWhatBugDbContext _context;

        public GetUsersAndRolesQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.ProjectId)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.ProjectId)))
                .MustAsync(Exist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GetUsersAndRolesQuery query, int projectId, CancellationToken cancellationToken)
        {
            return await _context.Projects.AnyAsync(p => p.Id == projectId);
        }
    }
}
