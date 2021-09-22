using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Admin.Commands.CreateRole;
using WhatBug.Domain.Data;
using WhatBug.WebUI.Authorization;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.Admin
{
    public class AdminController : BaseController
    {
        [HttpGet]
        [RequirePermission(Permissions.ManageProjectRoles)]
        public IActionResult CreateRole()
        {
            var command = new CreateRoleCommand();
            return View(command);
        }

        [HttpPost]
        [RequirePermission(Permissions.ManageProjectRoles)]
        public async Task<IActionResult> CreateRole(CreateRoleCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var result = await Mediator.Send(command);
            return null; // TODO: Return to index
        }
    }
}