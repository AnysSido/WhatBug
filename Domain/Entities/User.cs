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
        public int Id { get; set; }

        public List<UserPermission> UserPermissions { get; set; }
        public List<ProjectRoleUser> ProjectRoles { get; set; }
        public List<Issue> AssignedIssues { get; set; }
        public List<Issue> ReportedIssues { get; set; }
    }
}
