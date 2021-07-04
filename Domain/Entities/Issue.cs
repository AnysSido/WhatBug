using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Common;

namespace WhatBug.Domain.Entities
{
    public class Issue : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int ReporterId { get; set; }
        public User Reporter { get; set; }

        public int? AssigneeId { get; set; }
        public User Assignee { get; set; }
    }
}
