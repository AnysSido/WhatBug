using System.Collections.Generic;
using WhatBug.Domain.Common;
using WhatBug.Domain.Entities.Permissions;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Domain.Entities
{
    public class Project : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int PrioritySchemeId { get; set; }
        public PriorityScheme PriorityScheme { get; set; }

        public List<ProjectRoleUser> RoleUsers { get; set; } = new List<ProjectRoleUser>();
        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}
