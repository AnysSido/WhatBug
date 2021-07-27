using System.Collections.Generic;
using WhatBug.Domain.Common;
using WhatBug.Domain.Entities.JoinTables;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Entities
{
    public class Project : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int PrioritySchemeId { get; set; }
        public PriorityScheme PriorityScheme { get; set; }

        public List<ProjectUserProjectRole> ProjectRoleUsers { get; set; } = new List<ProjectUserProjectRole>();
        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}
