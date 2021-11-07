using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Infrastructure.Identity;

namespace WhatBug.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int Id { get; }
        public string Username { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string Surname { get; }
        public bool IsAuthenticated { get; }
        public bool IsReadOnly { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Id = int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.Id.ToString()), out var id) ? id : 0;
            Username = httpContextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.Username.ToString());
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.Email.ToString());
            FirstName = httpContextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.FirstName.ToString());
            Surname = httpContextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.Surname.ToString());
            IsReadOnly = httpContextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.WriteAccess.ToString()) == null;
            IsAuthenticated = Id > 0;
        }
    }
}
