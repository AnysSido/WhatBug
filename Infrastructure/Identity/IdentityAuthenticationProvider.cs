using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Settings;

namespace WhatBug.Infrastructure.Identity
{
    class IdentityAuthenticationProvider : IAuthenticationProvider
    {
        private readonly UserManager<PrincipalUser> _userManager;
        private readonly SignInManager<PrincipalUser> _signInManager;
        private readonly WhatBugSettings _settings;

        public IdentityAuthenticationProvider(UserManager<PrincipalUser> userManager, SignInManager<PrincipalUser> signInManager, IOptions<WhatBugSettings> settings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _settings = settings.Value;
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
            if (!_settings.Accounts.DemoEnabled)
                return false;

            var demoUsername = _settings.Accounts.DemoUsername;

            if (demoUsername == null || demoUsername.Length == 0)
                return false;

            var user = await _userManager.FindByNameAsync(demoUsername);
            
            if (user == null)
                return false;

            await _signInManager.SignInAsync(user, true);

            return true;
        }

        public async Task<bool> SignInAsync(string username, string password, bool rememberMe)
        {
            var user = await _userManager.FindByNameAsync(username) ?? await _userManager.FindByEmailAsync(username);

            if (user == null)
                return false;

            user.WriteAccess = true;

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
