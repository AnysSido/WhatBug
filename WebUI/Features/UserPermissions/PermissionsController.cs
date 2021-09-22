using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.UserPermissions.Commands.GrantGlobalPermissions;
using WhatBug.Application.UserPermissions.Queries.GetGlobalPermissions;
using WhatBug.Application.UserPermissions.Queries.GetGrantGlobalPermissions;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.UserPermissions.GrantGlobalPermissions;

namespace WhatBug.WebUI.Features.UserPermissions
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
