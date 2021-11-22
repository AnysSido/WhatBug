using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Issues.Commands.SetIssueStatus;
using WhatBug.Application.Projects.Queries.GetKanbanBoard;
using WhatBug.WebUI.Common;

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

        [HttpPost("/kanban/set-issue-status", Name = "SetKanbanIssueStatus")]
        public async Task<IActionResult> SetIssueStatus(string issueId, int issueStatusId)
        {
            await Mediator.Send(new SetIssueStatusCommand { IssueId = issueId, IssueStatusId = issueStatusId });

            return Json(new { success = true });
        }
    }
}
