﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme;
using WhatBug.Application.PermissionSchemes.Commands.GrantRolePermissions;
using WhatBug.Application.PermissionSchemes.Queries.GetCreatePermissionScheme;
using WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions;
using WhatBug.Application.PermissionSchemes.Queries.GetPermissionSchemes;
using WhatBug.Application.PermissionSchemes.Queries.GetSchemeRoles;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.PermissionSchemes.GrantRolePermissions;

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

        public async Task<IActionResult> Roles(int schemeId)
        {
            var dto = await Mediator.Send(new GetSchemeRolesQuery { SchemeId = schemeId });
            return View(dto);
        }

        public async Task<IActionResult> GetGrantRolePermissionsPartial(int schemeId, int roleId)
        {
            var dto = await Mediator.Send(new GetGrantRolePermissionsQuery { SchemeId = schemeId, RoleId = roleId });
            var vm = Mapper.Map<GrantRolePermissionsViewModel>(dto);
            return PartialView(vm);
        }

        public async Task<IActionResult> GrantRolePermissions(GrantRolePermissionsViewModel vm)
        {
            var command = new GrantRolePermissionsCommand { SchemeId = vm.Id, RoleId = vm.RoleId, PermissionIds = vm.GetPermissionIds() };
            await Mediator.Send(command);
            return RedirectToAction(nameof(Roles), new { schemeId = vm.Id });
        }
    }
}