using System.Threading.Tasks;

namespace WhatBug.Application.UserInfo
{
    public interface IUserInfoService
    {
        Task<UserInfoDto> GetUserInfoAsync(int userId);
    }
}