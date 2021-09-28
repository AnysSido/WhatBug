using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.ProjectRoles.Commands.CreateRole;
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
            var command = new CreateRoleCommand();
            return View(command);
        }

        [HttpPost("create", Name = "CreateRole")]
        [RequirePermission(Permissions.ManageProjectRoles)]
        public async Task<IActionResult> CreateRole(CreateRoleCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await Mediator.Send(command);
            return RedirectToAction(nameof(ProjectRoles));
        }
    }
}