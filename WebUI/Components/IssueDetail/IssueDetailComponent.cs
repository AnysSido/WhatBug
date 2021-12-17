using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Issues.Queries.GetIssueDetail;

namespace WhatBug.WebUI.Components.IssueDetail
{
    [ViewComponent(Name = "IssueDetail")]
    public class IssueDetailComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IssueDetailDTO vm)
        {
            return View(vm);
        }
    }
}
