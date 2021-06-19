using System.Threading.Tasks;
using WhatBug.Application.Common.Models;
using WhatBug.Application.Permissions;

namespace WhatBug.Application.Services.Interfaces
{
    interface IAuthorizationService
    {
        Task<Result> AddClaimAsync(int userId, ClaimType claimType, string claimValue);
        Task<bool> UserHasClaimAsync(int userId, ClaimType claimType, string claimValue = null);
    }
}