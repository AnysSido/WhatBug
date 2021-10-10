using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme;
using WhatBug.Application.PermissionSchemes.Commands.GrantRolePermissions;
using WhatBug.Application.PermissionSchemes.Queries.GetEditPermissionScheme;
using WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions;
using WhatBug.Application.PermissionSchemes.Queries.GetPermissionSchemes;
using WhatBug.Application.PermissionSchemes.Queries.GetSchemeRoles;
using WhatBug.Domain.Data;
using WhatBug.WebUI.Authorization;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.PermissionSchemes.GrantRolePermissions;
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

        [HttpGet("roles")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> Roles(int schemeId)
        {
            var dto = await Mediator.Send(new GetSchemeRolesQuery { SchemeId = schemeId });
            return View(dto);
        }

        [AjaxOnly]
        [HttpGet("getgrantrolepermissionspartial")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> GetGrantRolePermissionsPartial(int schemeId, int roleId)
        {
            var dto = await Mediator.Send(new GetGrantRolePermissionsQuery { SchemeId = schemeId, RoleId = roleId });
            var vm = Mapper.Map<GrantRolePermissionsViewModel>(dto);
            return PartialView(vm);
        }

        [HttpPost("grant", Name = "GrantRolePermissions")]
        [RequirePermission(Permissions.ManagePermissionSchemes)]
        public async Task<IActionResult> GrantRolePermissions(GrantRolePermissionsViewModel vm)
        {
            var command = new GrantRolePermissionsCommand { SchemeId = vm.Id, RoleId = vm.RoleId, PermissionIds = vm.GetPermissionIds() };
            await Mediator.Send(command);
            return RedirectToAction(nameof(Roles), new { schemeId = vm.Id });
        }
    }
}