using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Models;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Services
{
    class UserService : IUserService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IAuthenticationProvider _authenticationProvider;

        public UserService(IWhatBugDbContext context, IAuthenticationProvider authenticationProvider)
        {
            _context = context;
            _authenticationProvider = authenticationProvider;
        }

        public async Task<Result> CreateUserAsync(string username, string password)
        {
            // First create the principal user as this is where any credential validation is performed.
            var result = await _authenticationProvider.CreateUserAsync(username, password);
            if (!result.Succeeded)
                return result;

            // Create the application user.
            var user = new User();
            await _context.Users.AddAsync(user);
            if (await _context.SaveChangesAsync() == 0)
            {
                await _authenticationProvider.DeleteUserAsync(username);
                return Result.Failure(new string[] { $"An unknown error occured while creating user {username}." });
            }

            // Map the new application userId to the newly created principal user.
            result = await _authenticationProvider.SetUserId(username, user.Id);

            return result;
        }
    }
}
