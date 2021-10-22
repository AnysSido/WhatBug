using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme;
using WhatBug.Application.PermissionSchemes.Commands.DeletePermissionScheme;
using WhatBug.Application.PermissionSchemes.Commands.EditPermissionScheme;
using WhatBug.Application.PermissionSchemes.Commands.GrantRolePermissions;
using WhatBug.Application.PermissionSchemes.Queries.GetDeleteConfirm;
using WhatBug.Application.PermissionSchemes.Queries.GetEditPermissionScheme;
using WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions;
using WhatBug.Application.PermissionSchemes.Queries.GetPermissionSchemes;
using WhatBug.Application.PermissionSchemes.Queries.GetRolesAndPermissions;
using WhatBug.Domain.Data;
using WhatBug.WebUI.Authorization;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Routing;

namespace WhatBug.WebUI.Features.PermissionSchemes
{
    [Route("admin/permission-schemes", Name = "PermissionSchemes")]
    public class PermissionSchemesController : BaseController
    {
        [HttpGet("")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> Index()
        {
            var dto = await Mediator.Send(new GetPermissionSchemesQuery());
            return View(dto.Result);
        }

        [HttpGet("create", Name = "CreatePermissionScheme")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create", Name = "CreatePermissionScheme")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> Create(CreatePermissionSchemeCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.HasValidationErrors)
                return ViewWithErrors(command, result);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{schemeId}/edit", Name = "EditPermissionScheme")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> Edit(int schemeId)
        {
            var result = await Mediator.Send(new GetEditPermissionSchemeQuery { SchemeId = schemeId });

            return View(result.Result);
        }

        [HttpPost("{schemeId}/edit", Name = "EditPermissionScheme")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> Edit(GetEditPermissionSchemeQueryResult vm)
        {
            var result = await Mediator.Send(new EditPermissionSchemeCommand
            {
                SchemeId = vm.Id,
                Name = vm.Name,
                Description = vm.Description
            });

            if (result.HasValidationErrors)
                return ViewWithErrors(vm, result);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{schemeId}/roles-and-permissions", Name = "RolesAndPermissions")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> RolesAndPermissions(int schemeId)
        {
            var result = await Mediator.Send(new GetRolesAndPermissionsQuery { SchemeId = schemeId });

            return View(result.Result);
        }

        [AjaxOnly]
        [HttpGet("getgrantrolepermissionspartial")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> GetGrantRolePermissionsPartial(int schemeId, int roleId)
        {
            var result = await Mediator.Send(new GetGrantRolePermissionsQuery { SchemeId = schemeId, RoleId = roleId });
            return PartialView(result.Result);
        }

        [HttpPost("grant", Name = "GrantRolePermissions")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> GrantRolePermissions(GetGrantRolePermissionsQueryResult vm)
        {
            var command = new GrantRolePermissionsCommand
            {
                SchemeId = vm.Id,
                RoleId = vm.RoleId,
                PermissionIds = vm.Permissions.Where(p => p.IsGranted).Select(p => p.Id).ToList()
            };

            await Mediator.Send(command);
            return RedirectToAction(nameof(RolesAndPermissions), new { schemeId = vm.Id });
        }

        [AjaxOnly]
        [HttpGet("getdeleteconfirmpartial")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> GetDeleteConfirmPartial(int schemeId)
        {
            var result = await Mediator.Send(new GetDeleteConfirmQuery
            {
                SchemeId = schemeId
            });

            return PartialView(result.Result);
        }

        [HttpPost("delete", Name = "DeletePermissionScheme")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> Delete(int schemeId)
        {
            var result = await Mediator.Send(new DeletePermissionSchemeCommand
            {
                SchemeId = schemeId
            });

            return RedirectToAction(nameof(Index));
        }
    }
}