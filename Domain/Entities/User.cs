using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Common;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public List<Permission> GlobalPermissions = new List<Permission>();
        public List<ProjectRoleUser> ProjectRoles = new List<ProjectRoleUser>();
    }
}
