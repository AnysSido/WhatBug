using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Queries.GetKanbanBoard;
using WhatBug.WebUI.Controllers;

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
    }
}
