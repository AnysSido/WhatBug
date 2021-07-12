using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.DTOs.Issues
{
    public class CreateIssueDTO
    {
        public int ProjectId { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int ReporterId { get; set; }
        public int AssigneeId { get; set; }
        public int SchemePriorityId { get; set; }
    }
}
