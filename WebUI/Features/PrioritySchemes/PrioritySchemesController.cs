using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Commands.CreatePriorityScheme;
using WhatBug.Application.PrioritySchemes.Commands.DeletePriorityScheme;
using WhatBug.Application.PrioritySchemes.Commands.EditPriorityScheme;
using WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme;
using WhatBug.Application.PrioritySchemes.Queries.GetDeleteConfirm;
using WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme;
using WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes;
using WhatBug.Domain.Data;
using WhatBug.WebUI.Authorization;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Routing;

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
            var result = await Mediator.Send(new GetCreatePrioritySchemeQuery());
            return View(result.Result);
        }

        [HttpPost("create", Name = "CreatePriorityScheme")]
        [RequirePermission(Permissions.ManagePrioritySchemes)]
        public async Task<IActionResult> Create(CreatePrioritySchemeCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.HasValidationErrors)
            {
                var dto = await Mediator.Send(new GetCreatePrioritySchemeQuery());
                return ViewWithErrors(dto.Result, result);
            }

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

        [AjaxOnly]
        [HttpGet("getdeleteconfirmpartial")]
        [RequirePermission(Permissions.ManagePrioritySchemes)]
        public async Task<IActionResult> GetDeleteConfirmPartial(int schemeId)
        {
            var result = await Mediator.Send(new GetDeleteConfirmQuery
            {
                SchemeId = schemeId
            });

            return PartialView(result.Result);
        }

        [HttpPost("delete", Name = "DeletePriorityScheme")]
        [RequirePermission(Permissions.ManagePrioritySchemes)]
        public async Task<IActionResult> Delete(int schemeId)
        {
            var result = await Mediator.Send(new DeletePrioritySchemeCommand
            {
                SchemeId = schemeId
            });

            return RedirectToAction(nameof(Index));
        }
    }
}