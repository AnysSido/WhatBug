using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.ViewModels.Issues
{
    public class IssueDetailViewModel
    {
        public string IssueType { get; set; }
        public PriorityViewModel Priority { get; set; }
        public string Description { get; set; }
    }
}
