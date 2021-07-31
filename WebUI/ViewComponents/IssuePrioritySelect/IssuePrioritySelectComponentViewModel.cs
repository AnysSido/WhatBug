using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.ViewComponents
{
    public class IssuePrioritySelectComponentViewModel
    {
        [Display(Name = "Priority")]
        public int SelectedPriorityId { get; set; }
        public IList<PriorityViewModel> AllSchemePriorities { get; set; }
    }
}
