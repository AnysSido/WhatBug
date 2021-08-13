using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Permissions.Commands.GrantGlobalPermissions;
using WhatBug.Application.Permissions.Queries.GetGlobalPermissions;
using WhatBug.Application.Permissions.Queries.GetGrantGlobalPermissions;
using WhatBug.WebUI.Controllers;
using WhatBug.WebUI.Features.Permissions.GrantGlobalPermissions;

namespace WhatBug.WebUI.Features.Permissions
{
    public class PermissionsController : BaseController
    {
        public async Task<IActionResult> Global()
        {
            var dto = await Mediator.Send(new GetGlobalPermissionsQuery());
            return View(dto);
        }

        public async Task<IActionResult> GetGrantGlobalPermissionsPartial(int userId)
        {
            var dto = await Mediator.Send(new GetGrantGlobalPermissionsQuery { UserId = userId });
            var vm = Mapper.Map<GrantGlobalPermissionsViewModel>(dto);
            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> GrantGlobalPermissions(GrantGlobalPermissionsViewModel vm)
        {
            var command = new GrantGlobalPermissionsCommand { UserId = vm.UserId, PermissionIds = vm.GetPermissionIds() };
            await Mediator.Send(command);
            return RedirectToAction(nameof(Global));
        }
    }
}
