using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WhatBug.Application.Issues.Commands.SetIssueStatus;
using WhatBug.Application.Projects.Queries.GetKanbanBoard;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.Projects.KanbanBoard
{
    public class KanbanController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index(int projectId)
        {
            var dto = await Mediator.Send(new GetKanbanBoardQuery { ProjectId = projectId });

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> SetIssueStatus(string issueId, int issueStatusId)
        {
            await Mediator.Send(new SetIssueStatusCommand { IssueId = issueId, IssueStatusId = issueStatusId });

            return Json(new { success = true });
        }
    }
}
