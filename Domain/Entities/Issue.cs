﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Common;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Domain.Entities
{
    public class Issue : AuditableEntity
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int ReporterId { get; set; }
        public User Reporter { get; set; }

        public int? AssigneeId { get; set; }
        public User Assignee { get; set; }

        public int IssueTypeId { get; set; }
        public IssueType IssueType { get; set; }

        public int IssueStatusId { get; set; }
        public IssueStatus IssueStatus { get; set; }

        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
    }
}
