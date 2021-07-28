using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.DTOs.Projects
{
    public class AddUsersToProjectRoleDTO
    {
        public int ProjectId { get; set; }
        public IEnumerable<int> UserIds { get; set; } = new List<int>();
        public int ProjectRoleId { get; set; }
    }
}
