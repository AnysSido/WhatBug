using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Infrastructure.Identity;

namespace WhatBug.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public bool IsReadOnly { get; private set; }


        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;

            LoadClaims();
        }

        public void LoadClaims()
        {
            Id = int.TryParse(_contextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.Id.ToString()), out var id) ? id : 0;
            Username = _contextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.Username.ToString());
            Email = _contextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.Email.ToString());
            FirstName = _contextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.FirstName.ToString());
            Surname = _contextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.Surname.ToString());
            IsReadOnly = _contextAccessor.HttpContext?.User?.FindFirstValue(UserInfoClaim.WriteAccess.ToString()) == null;
            IsAuthenticated = Id > 0;
        }
    }
}
