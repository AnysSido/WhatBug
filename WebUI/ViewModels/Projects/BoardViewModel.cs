using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Issues;

namespace WhatBug.WebUI.ViewModels.Projects
{
    public class BoardViewModel
    {
        public IList<IssueStatusViewModel> IssueStatuses { get; set; }
    }
}
