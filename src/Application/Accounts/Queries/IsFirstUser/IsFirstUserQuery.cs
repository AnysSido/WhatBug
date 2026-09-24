using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;

namespace WhatBug.Application.Accounts.Queries.IsFirstUser
{
    [NoAuthorize]
    public record IsFirstUserQuery : IQuery<Response<bool>>
    {
    }

    public class IsFirstUserQueryHandler : IRequestHandler<IsFirstUserQuery, Response<bool>>
    {
        private readonly IWhatBugDbContext _context;

        public IsFirstUserQueryHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<bool>> Handle(IsFirstUserQuery request, CancellationToken cancellationToken)
        {
            return Response<bool>.Success(!await _context.Users.AnyAsync(cancellationToken));
        }
    }
}
