using System.Threading.Tasks;
using WhatBug.Application.Common.Models;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IAuthenticationProvider
    {
        Task<Result> CreateUserAsync(string username, string password);
        Task<Result> DeleteUserAsync(string username);
        Task<string> GetUsername(int userId);
        Task<Result> SetUserId(string username, int userId);
    }
}
