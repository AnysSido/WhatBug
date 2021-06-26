using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Infrastructure.Identity;

namespace WhatBug.Infrastructure.Identity
{
    public class PrincipalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<PrincipalUser>
    {
        public PrincipalUserClaimsPrincipalFactory(UserManager<PrincipalUser> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(PrincipalUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserId", user.UserId.ToString()));
            return identity;
        }
    }
}