using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.DTOs.Permissions
{
    public class SetUserProjectRoleDTO
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
    }
}
