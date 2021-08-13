using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Users;
using WhatBug.Application.Services;
using WhatBug.Infrastructure.Identity;

namespace WhatBug.Infrastructure.Identity
{
    class IdentityAuthenticationProvider : IAuthenticationProvider
    {
        private readonly UserManager<PrincipalUser> _userManager;
        private readonly IMapper _mapper;

        public IdentityAuthenticationProvider(UserManager<PrincipalUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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

        public async Task<UserDTO> PopulatePrincipleUserInfo(UserDTO userDTO)
        {
            var principalUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserId == userDTO.Id);
            _mapper.Map(principalUser, userDTO);
            return userDTO;
        }

        // This method could be very inefficient for large userbases.
        // As this application will not be used for real teams it will be fine.
        public async Task<IList<UserDTO>> PopulatePrincipleUsersInfo(IList<UserDTO> userDTOs)
        {
            var ids = userDTOs.Select(u => u.Id);
            var principleUsers = await _userManager.Users
                .Where(u => ids.Contains(u.UserId))
                .ToListAsync();

            foreach (var principalUser in principleUsers)
            {
                var userDTO = userDTOs.Where(u => u.Id == principalUser.UserId).Single();
                if (userDTO != null)
                {
                    _mapper.Map(principalUser, userDTO);
                }
            }

            return userDTOs;
        }
    }
}
