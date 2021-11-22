using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Issues.Commands.SetIssueStatus;
using WhatBug.Application.Projects.Queries.GetKanbanBoard;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Routing;

namespace WhatBug.WebUI.Features.Projects.KanbanBoard
{
    [Route("projects/{projectId}/kanban-board", Name = "KanbanBoard")]
    public class KanbanController : BaseController
    {
        [HttpGet("")]
        public async Task<IActionResult> Index(int projectId)
        {
            var result = await Mediator.Send(new GetKanbanBoardQuery { ProjectId = projectId });

            return View(result.Result);
        }

        [AjaxOnly]
        [HttpPost("/kanban/set-issue-status", Name = "SetKanbanIssueStatus")]
        public async Task<IActionResult> SetIssueStatus(string issueId, int issueStatusId)
        {
            var result = await Mediator.Send(new SetIssueStatusCommand { IssueId = issueId, IssueStatusId = issueStatusId });

            return Json(new { success = result.Succeeded });
        }
    }
}
