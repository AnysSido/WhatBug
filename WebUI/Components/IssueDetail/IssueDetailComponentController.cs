using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Issues.Commands.AddComment;
using WhatBug.Application.Issues.Commands.SetIssueDescription;
using WhatBug.Application.Issues.Commands.SetIssuePriority;
using WhatBug.Application.Issues.Commands.SetIssueSummary;
using WhatBug.Application.Issues.Commands.SetIssueType;
using WhatBug.Application.Issues.Queries.GetComments;
using WhatBug.Application.Issues.Queries.GetIssueDetail;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Components.IssueDetail
{
    public class IssueDetailComponentController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetComponent(string issueId)
        {
            var result = await Mediator.Send(new GetIssueDetailQuery { IssueId = issueId });
            return ViewComponent("IssueDetail", result.Result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSummary(string issueId, string summary)
        {
            var result = await Mediator.Send(new SetIssueSummaryCommand { IssueId = issueId, Summary = summary });
            return Json(new { success = result.Succeeded });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIssueType(string issueId, int issueTypeId)
        {
            var result = await Mediator.Send(new SetIssueTypeCommand { IssueId = issueId, IssueTypeId = issueTypeId });
            return Json(new { success = result.Succeeded });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePriority(string issueId, int priorityId)
        {
            var result = await Mediator.Send(new SetIssuePriorityCommand { IssueId= issueId, PriorityId = priorityId });
            return Json(new { success = result.Succeeded});
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDescription(string issueId, string description)
        {
            var result = await Mediator.Send(new SetIssueDescriptionCommand { IssueId = issueId, Description = description });
            return Json(new { success = result.Succeeded });
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string issueId, string comment)
        {
            var result = await Mediator.Send(new AddCommentCommand { IssueId = issueId, Content = comment });
            return Json(new { success = result.Succeeded });
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsPartial(string issueId)
        {
            var result = await Mediator.Send(new GetCommentsQuery { IssueId = issueId });
            return PartialView("/Components/IssueDetail/_Comments.cshtml", result.Result);
        }
    }
}
