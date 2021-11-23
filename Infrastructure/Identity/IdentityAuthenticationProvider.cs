using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Infrastructure.Identity
{
    class IdentityAuthenticationProvider : IAuthenticationProvider
    {
        private readonly UserManager<PrincipalUser> _userManager;
        private readonly SignInManager<PrincipalUser> _signInManager;

        public IdentityAuthenticationProvider(UserManager<PrincipalUser> userManager, SignInManager<PrincipalUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CreateUserAsync(string username, string password, string email, int id)
        {
            var user = new PrincipalUser()
            {
                UserName = username,
                Email = email,
                UserId = id
            };

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }

        public async Task<bool> SignInDemoAsync()
        {
            var user = await _userManager.FindByNameAsync("Anys"); // TODO: Move to config

            if (user == null)
                return false;

            user.IsReadOnly = true;

            await _signInManager.SignInAsync(user, true);

            return true;
        }

        public async Task<bool> SignInAsync(string username, string password, bool rememberMe)
        {
            var user = await _userManager.FindByNameAsync(username) ?? await _userManager.FindByEmailAsync(username);

            if (user == null)
                return false;

            var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);
            
            return result.Succeeded;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> GetUsername(int userId)
        {
            var principalUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            return principalUser?.UserName;
        }
    }
}
