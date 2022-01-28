using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.ProjectRoles.Commands.CreateRole;
using WhatBug.Application.ProjectRoles.Commands.DeleteRole;
using WhatBug.Application.ProjectRoles.Commands.EditRole;
using WhatBug.Application.ProjectRoles.Queries.GetDeleteRoleConfirm;
using WhatBug.Application.ProjectRoles.Queries.GetEditRole;
using WhatBug.Application.ProjectRoles.Queries.GetRoles;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Routing;

namespace WhatBug.WebUI.Features.ProjectRoles
{
    [Route("admin/project-roles", Name = "ProjectRoles")]
    public class ProjectRolesController : BaseController
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var queryResult = await Mediator.Send(new GetRolesQuery());
            return View(queryResult.Result);
        }

        [HttpGet("create", Name = "CreateRole")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost("create", Name = "CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.HasValidationErrors)
                return ViewWithErrors(command, result);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{roleId}/edit", Name = "EditRole")]
        public async Task<IActionResult> EditRole(int roleId)
        {
            var result = await Mediator.Send(new GetEditRoleQuery { RoleId = roleId });

            return View(result.Result);
        }

        [HttpPost("{roleId}/edit", Name = "EditRole")]
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

        [HttpPost("delete", Name = "DeleteRole")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            var result = await Mediator.Send(new DeleteRoleCommand
            {
                RoleId = roleId
            });

            return RedirectToAction(nameof(Index));
        }

        [AjaxOnly]
        [HttpGet("getdeleteroleconfirmpartial")]
        public async Task<IActionResult> GetDeleteRoleConfirmPartial(int roleId)
        {
            var result = await Mediator.Send(new GetDeleteRoleConfirmQuery
            {
                RoleId = roleId
            });

            return PartialView(result.Result);
        }
    }
}