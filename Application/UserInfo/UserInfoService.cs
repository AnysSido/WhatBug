using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.UserInfo
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public UserInfoService(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserInfoDto> GetUserInfoAsync(int userId)
        {
            if (userId == default)
                throw new ArgumentException(nameof(userId));

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            return _mapper.Map<UserInfoDto>(user);
        }
    }
}