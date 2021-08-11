using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Priorities.Commands.CreatePriority;
using WhatBug.Application.Priorities.Queries.GetCreatePriority;
using WhatBug.Application.Priorities.Queries.GetPriorities;
using WhatBug.Domain.Data;
using WhatBug.WebUI.Controllers;
using WhatBug.WebUI.Features.Priorities.Create;
using WhatBug.WebUI.Features.Priorities.Index;

namespace WhatBug.WebUI.Features.Priorities
{
    public class PriorityController : BaseController
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
    }
}
