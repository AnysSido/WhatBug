﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Models;

namespace WhatBug.Infrastructure.Identity
{
    class IdentityAuthenticationProvider : IAuthenticationProvider
    {
        private readonly UserManager<PrincipalUser> _userManager;

        public IdentityAuthenticationProvider(UserManager<PrincipalUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> CreateUserAsync(string username, string password)
        {
            var user = new PrincipalUser()
            {
                UserName = username,
                Email = username,
            };

            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description));
        }

        public async Task<Result> DeleteUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return Result.Failure(new string[] { $"User {username} not found." });

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description));
        }

        public async Task<Result> SetUserId(string username, int userId)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return Result.Failure(new string[] { $"User {username} not found." });

            user.UserId = userId;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description));
        }

        public async Task<string> GetUsername(int userId)
        {
            var principalUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            return principalUser?.UserName;
        }
    }
}
