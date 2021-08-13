using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme;
using WhatBug.Application.PermissionSchemes.Queries.GetCreatePermissionScheme;
using WhatBug.Application.PermissionSchemes.Queries.GetPermissionSchemes;
using WhatBug.WebUI.Controllers;

namespace WhatBug.WebUI.Features.PermissionSchemes
{
    public class PermissionSchemesController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var dto = await Mediator.Send(new GetPermissionSchemesQuery());
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var dto = await Mediator.Send(new GetCreatePermissionSchemeQuery());
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionSchemeCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}
