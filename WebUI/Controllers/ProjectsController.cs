using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities;
using WhatBug.Persistence;
using WhatBug.WebUI.ViewModels.PrioritySchemes;
using WhatBug.WebUI.ViewModels.Projects;

namespace WhatBug.WebUI.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPrioritySchemeService _prioritySchemeService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, ICurrentUserService currentUserService, IPrioritySchemeService prioritySchemeService, IMapper mapper)
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
            _prioritySchemeService = prioritySchemeService;
            _mapper = mapper;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _projectService.ListProjects());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateProjectViewModel()
            {
                PrioritySchemes = _mapper.Map<List<PrioritySchemeViewModel>>(await _prioritySchemeService.GetPrioritySchemesAsync())
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PrioritySchemes = _mapper.Map<List<PrioritySchemeViewModel>>(await _prioritySchemeService.GetPrioritySchemesAsync());
                return View(vm);
            }

            await _projectService.CreateProject(_mapper.Map<CreateProjectDTO>(vm));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("/Project/{projectId}/UsersAndRoles")]
        public async Task<IActionResult> UsersAndRoles(int projectId)
        {
            var vm = new ProjectUsersAndRolesViewModel
            {
                ProjectId = projectId
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> GetAddUserToProjectRolePartial(_AddUserToProjectRoleViewModel vm)
        {
            var project = await _projectService.GetProjectAsync(vm.ProjectId);
            return PartialView("_AddUserToProjectRolePartial", vm);
        }
    }
}
