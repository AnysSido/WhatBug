using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryValidator : AbstractValidator<GetUserProfileQuery>
    {
        private readonly IWhatBugDbContext _context;

        public GetUserProfileQueryValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.UserId)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.UserId)))
                .MustAsync(Exist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> Exist(GetUserProfileQuery query, int userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }
    }
}
