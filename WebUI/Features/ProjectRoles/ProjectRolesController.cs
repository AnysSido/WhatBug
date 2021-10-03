using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.ProjectRoles.Commands.CreateRole;
using WhatBug.Application.ProjectRoles.Commands.EditRole;
using WhatBug.Application.ProjectRoles.Queries.GetEditRole;
using WhatBug.Application.ProjectRoles.Queries.GetRoles;
using WhatBug.Domain.Data;
using WhatBug.WebUI.Authorization;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.ProjectRoles
{
    [Route("admin/project-roles", Name = "ProjectRoles")]
    public class ProjectRolesController : BaseController
    {
        [HttpGet("")]
        [RequirePermission(Permissions.ManageProjectRoles)]
        public async Task<IActionResult> Index()
        {
            var queryResult = await Mediator.Send(new GetRolesQuery());
            return View(queryResult.Result);
        }

        [HttpGet("create", Name = "CreateRole")]
        [RequirePermission(Permissions.ManageProjectRoles)]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost("create", Name = "CreateRole")]
        [RequirePermission(Permissions.ManageProjectRoles)]
        public async Task<IActionResult> CreateRole(CreateRoleCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.HasValidationErrors)
                return ViewWithErrors(command, result);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{roleId}/edit", Name = "EditRole")]
        [RequirePermission(Permissions.ManageProjectRoles)]
        public async Task<IActionResult> EditRole(int roleId)
        {
            var result = await Mediator.Send(new GetEditRoleQuery { RoleId = roleId });

            return View(result.Result);
        }

        [HttpPost("{roleId}/edit", Name = "EditRole")]
        [RequirePermission(Permissions.ManageProjectRoles)]
        public async Task<IActionResult> EditRole(GetEditRoleQueryResult vm)
        {
            var result = await Mediator.Send(new EditRoleCommand
            {
                RoleId = vm.Id,
                Name = vm.Name,
                Description = vm.Description
            });

            if (result.HasValidationErrors)
                return ViewWithErrors(vm, result);

            return RedirectToAction(nameof(Index));
        }
    }
}