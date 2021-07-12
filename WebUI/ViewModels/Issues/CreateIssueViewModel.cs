using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.ViewModels.Issues
{
    public class CreateIssueViewModel
    {
        [HiddenInput]
        public int ProjectId { get; set; }
        [Required]
        public string Summary { get; set; }
        public string Description { get; set; }
        public int ReporterId { get; set; }
        public int AssigneeId { get; set; }
        [Display(Name = "Priority")]
        public int SchemePriorityId { get; set; }

        public List<PriorityViewModel> AllSchemePriorities { get; set; }
    }
}
