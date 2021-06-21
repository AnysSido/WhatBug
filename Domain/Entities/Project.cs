using System.Collections.Generic;
using WhatBug.Domain.Common;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Entities
{
    public class Project : AuditableEntity
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ProjectUserRole> ProjectUserRoles = new List<ProjectUserRole>();
    }
}
