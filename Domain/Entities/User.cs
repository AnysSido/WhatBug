using System.Collections.Generic;
using WhatBug.Domain.Entities.JoinTables;

namespace WhatBug.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public List<ProjectRoleUser> ProjectRoles { get; set; }
        public List<Issue> AssignedIssues { get; set; }
        public List<Issue> ReportedIssues { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
    }
}
