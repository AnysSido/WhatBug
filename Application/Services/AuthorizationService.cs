using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Models;
using WhatBug.Application.Permissions;
using WhatBug.Application.Services.Interfaces;

namespace WhatBug.Application.Services
{
    class AuthorizationService : IAuthorizationService
    {
        private readonly IWhatBugDbContext _context;

        public AuthorizationService(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserHasClaimAsync(int userId, ClaimType claimType, string claimValue = null)
        {
            var userClaim = await GetClaimAsync(userId, claimType, claimValue);
            return userClaim != null;
        }

        public async Task<Result> AddClaimAsync(int userId, ClaimType claimType, string claimValue)
        {
            var userClaim = await GetClaimAsync(userId, claimType, claimValue);
            if (userClaim != null)
                return Result.Failure(new string[] { $"User {userId} already has claim {claimType}." });

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return Result.Failure(new string[] { $"Failed to add claim {claimType}, user {userId} not found." });

            userClaim = new UserClaim() { UserId = user.UserId, ClaimType = claimType, ClaimValue = claimValue };
            await _context.UserClaims.AddAsync(userClaim);
            if (await _context.SaveChangesAsync() > 0)
                return Result.Success();

            return Result.Failure(new string[] { $"Failed to add claim {claimType} to user {userId}. Could not update database." });
        }

        private async Task<UserClaim> GetClaimAsync(int entityId, ClaimType claimType, string claimValue)
        {
            return await _context.UserClaims.FirstOrDefaultAsync(c => c.UserId == entityId && c.ClaimType == claimType && (claimValue == null || c.ClaimValue == claimValue));
        }
    }
}
