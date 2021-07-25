using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.Users;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<bool> UserHasPermission(int userId, string permission);
    }
}
