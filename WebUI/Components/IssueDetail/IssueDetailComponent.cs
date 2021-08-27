using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Components.IssueDetail
{
    [ViewComponent(Name = "IssueDetail")]
    public class IssueDetailComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IssueDetailViewModel vm)
        {
            return View(vm);
        }
    }
}
