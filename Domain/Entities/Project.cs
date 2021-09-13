using System.Collections.Generic;
using WhatBug.Domain.Common;

namespace WhatBug.Domain.Entities
{
    public class Project : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public int IssueCounter { get; set; }

        public int PrioritySchemeId { get; set; }
        public PriorityScheme PriorityScheme { get; set; }

        public int? PermissionSchemeId { get; set; }
        public PermissionScheme PermissionScheme { get; set; }

        public List<ProjectRoleUser> RoleUsers { get; set; }
        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}
