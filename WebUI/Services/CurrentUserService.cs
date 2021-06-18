using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserId { get; }
        public int Username { get; }
        public bool IsAuthenticated { get; }     

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId"), out var id) ? id : 0;
            Username = int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var username) ? username : 0;
            IsAuthenticated = Username > 0;
        }
    }
}
