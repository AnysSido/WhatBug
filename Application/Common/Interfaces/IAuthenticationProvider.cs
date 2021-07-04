﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Users;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IAuthenticationProvider
    {
        Task<Result> CreateUserAsync(string username, string password);
        Task<Result> DeleteUserAsync(string username);
        Task<UserDTO> PopulatePrincipleUserInfo(UserDTO userDTO);
        Task<List<UserDTO>> PopulatePrincipleUsersInfo(List<UserDTO> userDTOs);
        Task<Result> SetUserId(string username, int userId);
    }
}
