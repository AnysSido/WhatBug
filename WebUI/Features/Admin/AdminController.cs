using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Admin.Commands.CreateRole;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.Admin
{
    public class AdminController : BaseController
    {
        [HttpGet]
        public IActionResult CreateRole()
        {
            var command = new CreateRoleCommand();
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleCommand command)
        {
            await Mediator.Send(command);
            return null;
        }
    }
}
