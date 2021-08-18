using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Issues.Commands.SetIssueDescription;
using WhatBug.Application.Issues.Queries.GetIssueDetail;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Components.IssueDetail
{
    public class IssueDetailComponentController : BaseController
    {
        public async Task<IActionResult> GetComponent(string issueId)
        {
            var dto = await Mediator.Send(new GetIssueDetailQuery { IssueId = issueId });
            var vm = Mapper.Map<IssueDetailViewModel>(dto);
            return ViewComponent("IssueDetail", vm);
        }

        public async Task<IActionResult> UpdateDescription(string issueId, string description)
        {
            await Mediator.Send(new SetIssueDescriptionCommand { IssueId = issueId, Description = description });
            return Json(new { success = true });
        }
    }
}
