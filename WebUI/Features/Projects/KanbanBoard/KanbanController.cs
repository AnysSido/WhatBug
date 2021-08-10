using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Queries.GetKanbanBoard;
using WhatBug.WebUI.Controllers;

namespace WhatBug.WebUI.Features.Projects.KanbanBoard
{
    public class KanbanController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Board(int projectId)
        {
            var dto = await Mediator.Send(new GetKanbanBoardQuery { ProjectId = projectId });
            var vm = Mapper.Map<KanbanBoardViewModel>(dto);

            return View(vm);
        }
    }
}
