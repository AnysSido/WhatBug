using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Commands.CreatePriorityScheme;
using WhatBug.Application.PrioritySchemes.Commands.EditPriorityScheme;
using WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme;
using WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme;
using WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.PrioritySchemes.Create;
using WhatBug.WebUI.Features.PrioritySchemes.Edit;

namespace WhatBug.WebUI.Features.PrioritySchemes
{
    public class PrioritySchemesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dto = await Mediator.Send(new GetPrioritySchemesQuery());
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var dto = await Mediator.Send(new GetCreatePrioritySchemeQuery());
            var vm = Mapper.Map<CreatePrioritySchemeViewModel>(dto);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePrioritySchemeCommand command)
        {
            // TODO: Check modelstate
            await Mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await Mediator.Send(new GetEditPrioritySchemeQuery { Id = id });
            var vm = Mapper.Map<EditPrioritySchemeViewModel>(dto);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPrioritySchemeCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}
