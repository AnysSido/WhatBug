using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetKanbanBoard
{
    public class IssueOverviewDTO : IMapFrom<Issue>
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int IssueStatusId { get; set; }
        public string IssueStatusName { get; set; }
    }
}
