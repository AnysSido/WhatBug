using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.UserPermissions.Commands.GrantUserPermissions;
using WhatBug.Application.UserPermissions.Queries.GetGrantUserPermissions;
using WhatBug.Application.UserPermissions.Queries.GetUsersAndPermissions;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Routing;

namespace WhatBug.WebUI.Features.UserPermissions
{
    [Route("admin/user-permissions", Name = "UserPermissions")]
    public class UserPermissionsController : BaseController
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var result = await Mediator.Send(new GetUsersAndPermissionsQuery());
            return View(result.Result);
        }

        [HttpPost("grant", Name = "GrantUserPermissions")]
        public async Task<IActionResult> GrantUserPermissions(GetGrantUserPermissionsQueryResult vm)
        {
            var result = await Mediator.Send(new GrantUserPermissionsCommand
            {
                UserId = vm.Id,
                PermissionIds = vm.Permissions.Where(p => p.IsGranted).Select(p => p.Id).ToList()
            });

            return RedirectToAction(nameof(Index));
        }

        [AjaxOnly]
        [HttpGet("getgrantuserpermissionspartial")]
        public async Task<IActionResult> GetGrantUserPermissionsPartial(int userId)
        {
            var result = await Mediator.Send(new GetGrantUserPermissionsQuery
            {
                UserId = userId
            });

            return PartialView(result.Result);
        }
    }
}