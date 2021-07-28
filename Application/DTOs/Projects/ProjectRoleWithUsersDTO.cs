using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Users;

namespace WhatBug.Application.DTOs.Projects
{
    public class ProjectRoleWithUsersDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<UserDTO> Users = new List<UserDTO>();
    }
}
