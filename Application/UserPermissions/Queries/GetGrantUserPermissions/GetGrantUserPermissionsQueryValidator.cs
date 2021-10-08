using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.UserPermissions.Queries.GetGrantUserPermissions
{
    public class GetGrantUserPermissionsQueryValidator : AbstractValidator<GetGrantUserPermissionsQuery>
    {
        private IWhatBugDbContext _context;

        public GetGrantUserPermissionsQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.UserId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.UserId)))
                .MustAsync(Exist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GetGrantUserPermissionsQuery query, int userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(r => r.Id == userId);
        }
    }
}
