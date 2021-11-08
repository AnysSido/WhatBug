using System.Threading.Tasks;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IAuthenticationProvider
    {
        Task<bool> CreateUserAsync(string username, string password, string email, int id);
        Task<string> GetUsername(int userId);
        Task<bool> SignInAsync(string username, string password, bool rememberMe);
        Task<bool> SignInDemoAsync();
        Task SignOutAsync();
    }
}
