using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewComponents;

namespace WhatBug.WebUI.Controllers
{
    /*
     * View components used in this controller are intended to be initialized from javascript.
     * They are self-contained components that handle their own creation and functionality.
    
     */
    public class ComponentsController : Controller
    {
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
        public async Task<IActionResult> GetIssuePrioritySelectComponent(int projectId)
        {
            return ViewComponent("IssuePrioritySelect", new { projectId });
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
            return Json(new { success = true });
        }
    }
}
