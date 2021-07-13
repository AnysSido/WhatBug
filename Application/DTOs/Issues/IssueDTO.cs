using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.DTOs.Users;

namespace WhatBug.Application.DTOs.Issues
{
    public class IssueDTO
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        
        public int? AssigneeId { get; set; }
        public UserDTO Assignee { get; set; }

        public int ReporterId { get; set; }
        public UserDTO Reporter { get; set; }

        public int PriorityId { get; set; }
        public PriorityDTO Priority { get; set; }
    }
}
