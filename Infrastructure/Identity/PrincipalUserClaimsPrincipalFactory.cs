using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using WhatBug.Application.Users.Queries.GetUserInfo;

namespace WhatBug.Infrastructure.Identity
{
    public enum UserInfoClaim { Id, Username, Email, FirstName, Surname }

    public class PrincipalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<PrincipalUser>
    {
        private readonly IMediator _mediator;

        public PrincipalUserClaimsPrincipalFactory(UserManager<PrincipalUser> userManager, IOptions<IdentityOptions> optionsAccessor, IMediator mediator)
            : base(userManager, optionsAccessor)
        {
            _mediator = mediator;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(PrincipalUser user)
        {
            var userInfo = await _mediator.Send(new GetUserInfoQuery { UserId = user.UserId });
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim(UserInfoClaim.Id.ToString(), userInfo.Id.ToString()));
            identity.AddClaim(new Claim(UserInfoClaim.Username.ToString(), userInfo.Username));
            identity.AddClaim(new Claim(UserInfoClaim.Email.ToString(), userInfo.Email));
            identity.AddClaim(new Claim(UserInfoClaim.FirstName.ToString(), userInfo.FirstName));
            identity.AddClaim(new Claim(UserInfoClaim.Surname.ToString(), userInfo.Surname));

            return identity;
        }
    }
}