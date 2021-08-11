using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Commands.CreatePriorityScheme;
using WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme;
using WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes;
using WhatBug.WebUI.Controllers;
using WhatBug.WebUI.Features.PrioritySchemes.Create;
using WhatBug.WebUI.Features.PrioritySchemes.Index;

namespace WhatBug.WebUI.Features.PrioritySchemes
{
    public class PrioritySchemeController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dto = await Mediator.Send(new GetPrioritySchemesQuery());
            var vm = Mapper.Map<PrioritySchemesViewModel>(dto);
            return View(vm);
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
            await Mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}
