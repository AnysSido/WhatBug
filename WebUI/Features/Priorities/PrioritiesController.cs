using Microsoft.AspNetCore.Mvc;
using System;
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
using WhatBug.WebUI.Controllers;
using WhatBug.WebUI.Features.Priorities.Create;
using WhatBug.WebUI.Features.Priorities.Edit;
using WhatBug.WebUI.Features.Priorities.Index;

namespace WhatBug.WebUI.Features.Priorities
{
    public class PrioritiesController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var dto = await Mediator.Send(new GetPrioritiesQuery());
            var vm = Mapper.Map<PrioritiesViewModel>(dto);

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var dto = await Mediator.Send(new GetCreatePriorityQuery());
            var vm = Mapper.Map<CreatePriorityViewModel>(dto);
            vm.ColorId = vm.Colors.Single(c => c.Id == Colors.Rose.Id).Id;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePriorityCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await Mediator.Send(new GetEditPriorityQuery { Id = id });
            var vm = Mapper.Map<EditPriorityViewModel>(dto);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPriorityCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder([FromBody] List<int> ids)
        {
            await Mediator.Send(new ReorderPrioritiesCommand { Ids = ids });
            // TODO: Handle errors
            return Json(new { success = true, text = "Success!" });
        }
    }
}
