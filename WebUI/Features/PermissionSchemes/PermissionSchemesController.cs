using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
    }
}
