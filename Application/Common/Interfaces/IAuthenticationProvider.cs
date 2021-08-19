using System.Threading.Tasks;
using WhatBug.Application.Common.Models;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IAuthenticationProvider
    {
        Task<bool> CreateUserAsync(string username, string password, string email, int id);
        Task<Result> DeleteUserAsync(string username);
        Task<string> GetUsername(int userId);
        Task<Result> SetUserId(string username, int userId);
    }
}
