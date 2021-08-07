using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewComponents;

namespace WhatBug.WebUI.Controllers
{
    /*
     * View components used in this controller are intended to be initialized from javascript.
     * They are self-contained components that handle their own creation and functionality.
    
     */
    public class ComponentsController : Controller
    {
        private readonly IIssueService _issuesService;

        public ComponentsController(IIssueService issuesService)
        {
            _issuesService = issuesService;
        }

        /*
         * Initialize this component from javascript with 'new CreateIssueComponent();'
         */
        [HttpGet]
        public async Task<IActionResult> GetCreateIssueComponent()
        {
            return ViewComponent("CreateIssue");
        }

        /*
         * Initialize this component from javascript with 'new IssuePrioritySelectComponent();'
         */
        [HttpGet]
        public async Task<IActionResult> GetIssuePrioritySelectComponent(IssuePrioritySelectComponentOptions options)
        {
            return ViewComponent("IssuePrioritySelect", options);
        }

        /*
         * Initialize this component from javascript with 'new UserSelectorComponent();'
         */
        [HttpGet]
        public async Task<IActionResult> GetUserSelectorComponent(UserSelectorComponentOptions options)
        {
            return ViewComponent("UserSelector", options);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue(CreateIssueComponentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent("CreateIssue", vm);
            }

            // Create the issue here
            var issue = new CreateIssueDTO
            {
                ProjectId = vm.SelectedProjectId,
                Summary = vm.Summary,
                Description = vm.Description,
                PriorityId = vm.Priority.SelectedPriorityId,
                IssueTypeId = vm.SelectedIssueType,
                AssigneeId = vm.Assignee?.SelectedUserId,
                ReporterId = vm.Reporter?.SelectedUserId
            };

            await _issuesService.CreateIssueAsync(issue);

            if (vm.CreateAnother)
            {
                vm.PrepareCreateAnother();
                ModelState.Clear();               
                return ViewComponent("CreateIssue", vm);
            }

            return Json(new { success = true });
        }
    }
}
