using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Infrastructure.Identity;

namespace Infrastructure.Identity
{
    class PrincipalUserManager : IPrincipalUserManager
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<PrincipalUser> _userManager;

        public PrincipalUserManager(AppIdentityDbContext context, UserManager<PrincipalUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> UsernameExists(string username)
        {
            var normalizedUsername = _userManager.NormalizeName(username);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUsername);

            return user != null;
        }
    }
}
