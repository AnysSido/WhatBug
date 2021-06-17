using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Models;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IPrincipalUserManager
    {
        Task<Result> CreateUserAsync(string username, string password);
        Task<Result> DeleteUserAsync(string username);
        Task<Result> SetUserId(string username, int userId);
    }
}
