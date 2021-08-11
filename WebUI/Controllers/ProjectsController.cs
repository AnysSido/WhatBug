using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.Routing;
using WhatBug.WebUI.ViewModels.Admin;
using WhatBug.WebUI.ViewModels.Projects;
using WhatBug.WebUI.ViewModels.User;

namespace WhatBug.WebUI.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;
        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, IMapper mapper, IUserService userService, IAdminService adminService, IIssueService issueService)
        {
            _projectService = projectService;
            _mapper = mapper;
            _userService = userService;
            _adminService = adminService;
            _issueService = issueService;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _projectService.ListProjects());
        }

        [HttpGet]
        [Route("/Project/{projectId}/UsersAndRoles")]
        [RouteCategory(RouteCategory.Project)]
        public async Task<IActionResult> UsersAndRoles(int projectId)
        {
            var project = await _projectService.GetProjectAsync(projectId);
            var vm = new ProjectUsersAndRolesViewModel
            {
                ProjectId = projectId,
                ProjectName = project.Name,
                ProjectRolesWithUsers = _mapper.Map<List<ProjectRoleWithUsersViewModel>>(await _projectService.GetProjectRolesWithUsersAsync(projectId))
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> GetAddUserToProjectRolePartial(_AddUserToProjectRoleViewModel vm)
        {
            var project = _mapper.Map<ProjectViewModel>(await _projectService.GetProjectAsync(vm.ProjectId));
            vm.ProjectName = project.Name;
            vm.Users = _mapper.Map<List<UserViewModel>>(await _userService.GetAllUsersAsync());
            vm.ProjectRoles = _mapper.Map<List<ProjectRoleViewModel>>(await _adminService.GetProjectRolesAsync());

            return PartialView("_AddUserToProjectRolePartial", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsersToProjectRole(_AddUserToProjectRoleViewModel vm)
        {
            await _projectService.AddUsersToProjectRoleAsync(_mapper.Map<AddUsersToProjectRoleDTO>(vm));
            return RedirectToAction(nameof(UsersAndRoles), new { projectId = vm.ProjectId });
        }
    }
}
