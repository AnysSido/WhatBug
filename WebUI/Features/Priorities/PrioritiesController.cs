using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Priorities.Commands.CreatePriority;
using WhatBug.Application.Priorities.Commands.EditPriority;
using WhatBug.Application.Priorities.Commands.ReorderPriorities;
using WhatBug.Application.Priorities.Queries.GetCreatePriority;
using WhatBug.Application.Priorities.Queries.GetEditPriority;
using WhatBug.Application.Priorities.Queries.GetPriorities;
using WhatBug.Domain.Data;
using WhatBug.WebUI.Authorization;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Routing;

namespace WhatBug.WebUI.Features.Priorities
{
    [Route("admin/priorities", Name = "Priorities")]
    public class PrioritiesController : BaseController
    {
        [HttpGet("")]
        [RequirePermission(Permissions.ManagePriorities)]
        public async Task<IActionResult> Index()
        {
            var dto = await Mediator.Send(new GetPrioritiesQuery());

            return View(dto.Result);
        }

        [HttpGet("create", Name = "CreatePriority")]
        [RequirePermission(Permissions.ManagePriorities)]
        public async Task<IActionResult> Create()
        {
            var result = await Mediator.Send(new GetCreatePriorityQuery());

            result.Result.ColorId = result.Result.Colors.Single(c => c.Id == Colors.Rose.Id).Id;
            result.Result.IconId = result.Result.Icons.Single(i => i.Id == Icons.Exclaimation.Id).Id;
            return View(result.Result);
        }

        [HttpPost("create", Name = "CreatePriority")]
        [RequirePermission(Permissions.ManagePriorities)]
        public async Task<IActionResult> Create(GetCreatePriorityQueryResult vm)
        {
            var result = await Mediator.Send(new CreatePriorityCommand 
            {
                Name = vm.Name,
                Description = vm.Description,
                ColorId = vm.ColorId,
                IconId = vm.IconId
            });

            if (result.HasValidationErrors)
            {
                var dto = await Mediator.Send(new GetCreatePriorityQuery());
                return ViewWithErrors(dto.Result, result);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{priorityId}/edit", Name = "EditPriority")]
        [RequirePermission(Permissions.ManagePriorities)]
        public async Task<IActionResult> Edit(int priorityId)
        {
            var dto = await Mediator.Send(new GetEditPriorityQuery { Id = priorityId });
            return View(dto.Result);
        }

        [HttpPost("{priorityId}/edit", Name = "EditPriority")]
        [RequirePermission(Permissions.ManagePriorities)]
        public async Task<IActionResult> Edit(GetEditPriorityQueryResult vm)
        {
            var result = await Mediator.Send(new EditPriorityCommand
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
                ColorId = vm.ColorId,
                IconId = vm.IconId
            });

            if (result.HasValidationErrors)
            {
                var dto = await Mediator.Send(new GetEditPriorityQuery { Id = vm.Id });
                return ViewWithErrors(dto.Result, result);
            }

            return RedirectToAction(nameof(Index));
        }

        [AjaxOnly]
        [HttpPost("updatepriorityorder")]
        [RequirePermission(Permissions.ManagePriorities)]
        public async Task<IActionResult> UpdateOrder([FromBody] List<int> priorityIds)
        {
            var result = await Mediator.Send(new ReorderPrioritiesCommand { Ids = priorityIds });

            if (result.Succeeded)
                return Json(new { success = true });

            return Json(new { success = false, text = "Oops! Something went wrong." });
        }
    }
}