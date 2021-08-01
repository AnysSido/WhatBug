﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Issues;
using WhatBug.WebUI.ViewModels.Projects;
using WhatBug.WebUI.ViewModels.User;

namespace WhatBug.WebUI.ViewComponents
{
    public class CreateIssueComponentViewModel
    {
        [Required]
        public string Summary { get; set; }
        public string Description { get; set; }
        public int ReporterId { get; set; }

        [Display(Name = "Assignee")]
        public int AssigneeId { get; set; }
        public IList<UserViewModel> AllUsers { get; set; }

        [Display(Name = "Project")]
        public int SelectedProjectId { get; set; }
        public IList<ProjectViewModel> AllProjects { get; set; }

        [Display(Name = "Priority")]
        public int SelectedPriorityId { get; set; }

        [Display(Name = "Issue Type")]
        public int SelectedIssueType { get; set; }
        public IList<IssueTypeViewModel> AllIssueTypes { get; set; }
    }
}
