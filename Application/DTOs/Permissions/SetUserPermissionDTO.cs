using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Application.DTOs.Permissions
{
    public class SetUserPermissionDTO
    {
        public int UserId { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
