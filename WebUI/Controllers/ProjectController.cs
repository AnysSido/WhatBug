using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Issue;
using WhatBug.WebUI.ViewModels.Project;

namespace WebUI.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int id)
        {
            var dto = await _projectService.GetProject(id);
            var vm = new ProjectIndexViewModel()
            {
                Project = _mapper.Map<ProjectViewModel>(dto)
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult CreateIssue(int projectId)
        {
            var vm = new CreateIssueViewModel()
            {
                Issue = new IssueViewModel(),
                ProjectId = projectId
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue(CreateIssueViewModel vm)
        {
            await _projectService.CreateIssue(vm.ProjectId, _mapper.Map<IssueDTO>(vm.Issue));
            return View(vm);
        }
    }

    
}
