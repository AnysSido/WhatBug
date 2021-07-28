using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Domain.Entities.JoinTables
{
    public class ProjectUserProjectRole
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProjectRoleId { get; set; }
        public ProjectRole ProjectRole { get; set; }
    }
}
