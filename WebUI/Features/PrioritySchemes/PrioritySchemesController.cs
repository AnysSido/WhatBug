using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Commands.CreatePriorityScheme;
using WhatBug.Application.PrioritySchemes.Commands.EditPriorityScheme;
using WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme;
using WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme;
using WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes;
using WhatBug.Domain.Data;
using WhatBug.WebUI.Authorization;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.PrioritySchemes.Create;

namespace WhatBug.WebUI.Features.PrioritySchemes
{
    [Route("admin/priority-schemes", Name = "PrioritySchemes")]
    public class PrioritySchemesController : BaseController
    {
        [HttpGet("")]
        [RequirePermission(Permissions.ManagePrioritySchemes)]
        public async Task<IActionResult> Index()
        {
            var result = await Mediator.Send(new GetPrioritySchemesQuery());
            return View(result.Result);
        }

        [HttpGet("create", Name = "CreatePriorityScheme")]
        [RequirePermission(Permissions.ManagePrioritySchemes)]
        public async Task<IActionResult> Create()
        {
            var dto = await Mediator.Send(new GetCreatePrioritySchemeQuery());
            var vm = Mapper.Map<CreatePrioritySchemeViewModel>(dto);
            return View(vm);
        }

        [HttpPost("create", Name = "CreatePriorityScheme")]
        [RequirePermission(Permissions.ManagePrioritySchemes)]
        public async Task<IActionResult> Create(CreatePrioritySchemeCommand command)
        {
            // TODO: Check modelstate
            await Mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{schemeId}/edit", Name = "EditPriorityScheme")]
        [RequirePermission(Permissions.ManagePrioritySchemes)]
        public async Task<IActionResult> Edit(int schemeId)
        {
            var result = await Mediator.Send(new GetEditPrioritySchemeQuery { Id = schemeId });
            return View(result.Result);
        }

        [HttpPost("{schemeId}/edit", Name = "EditPriorityScheme")]
        [RequirePermission(Permissions.ManagePrioritySchemes)]
        public async Task<IActionResult> Edit(EditPrioritySchemeCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.HasValidationErrors)
            {
                var dto = await Mediator.Send(new GetEditPrioritySchemeQuery { Id = command.Id });
                return ViewWithErrors(dto.Result, result);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}