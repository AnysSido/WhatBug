using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using WhatBug.Application.UserInfo;

namespace WhatBug.Infrastructure.Identity
{
    public enum UserInfoClaim { Id, Username, Email, FirstName, Surname, WriteAccess }

    public class PrincipalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<PrincipalUser>
    {
        private readonly IUserInfoService _userInfoService;

        public PrincipalUserClaimsPrincipalFactory(UserManager<PrincipalUser> userManager, IOptions<IdentityOptions> optionsAccessor, IMediator mediator, IUserInfoService userInfoService)
            : base(userManager, optionsAccessor)
        {
            _userInfoService = userInfoService;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(PrincipalUser user)
        {
            var userInfo = await _userInfoService.GetUserInfoAsync(user.UserId);
            var identity = await base.GenerateClaimsAsync(user);

            if (userInfo != null)
            {
                identity.AddClaim(new Claim(UserInfoClaim.Id.ToString(), userInfo.Id.ToString()));
                identity.AddClaim(new Claim(UserInfoClaim.Username.ToString(), userInfo.Username));
                identity.AddClaim(new Claim(UserInfoClaim.Email.ToString(), userInfo.Email));
                identity.AddClaim(new Claim(UserInfoClaim.FirstName.ToString(), userInfo.FirstName));
                identity.AddClaim(new Claim(UserInfoClaim.Surname.ToString(), userInfo.Surname));
            }

            if (!user.IsReadOnly)
                identity.AddClaim(new Claim(UserInfoClaim.WriteAccess.ToString(), "true"));

            return identity;
        }
    }
}