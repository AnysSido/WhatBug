using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Users;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result> CreateUserAsync(string username, string password);
        Task<List<UserWithPermissionsDTO>> GetAllUsersWithPermissions();
        Task<UserWithPermissionsDTO> GetUserWithPermissions(int id);
    }
}
