using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Components.CreateIssue
{
    [ViewComponent(Name = "CreateIssue")]
    public class CreateIssueComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CreateIssueViewModel vm)
        {
            return View(vm);
        }
    }
}
