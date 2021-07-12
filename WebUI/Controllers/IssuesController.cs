using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Issues;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssueService _issueService;
        private readonly IProjectService _projectService;
        private readonly IPriorityIconService _priorityIconService;
        private readonly IMapper _mapper;

        public IssuesController(IIssueService issueService, IMapper mapper, IProjectService projectService, IPriorityIconService priorityIconService)
        {
            _issueService = issueService;
            _mapper = mapper;
            _projectService = projectService;
            _priorityIconService = priorityIconService;
        }

        [HttpGet]
        [Route("Projects/{projectId}/AllIssues")]
        public async Task<IActionResult> Index(int projectId)
        {
            var vm = new AllIssuesViewModel()
            {
                Issues = _mapper.Map<List<IssueViewModel>>(await _issueService.GetAllIssuesAsync(projectId))
            };
            return View(vm);
        }

        [HttpGet]
        [Route("Projects/{projectId}/CreateIssue")]
        public async Task<IActionResult> Create(int projectId)
        {
            var project = await _projectService.GetProjectAsync(projectId);
            var vm = new CreateIssueViewModel()
            {
                AllSchemePriorities = _mapper.Map<List<PriorityViewModel>>(project.PriorityScheme.Priorities)
            };
            vm.AllSchemePriorities.ForEach(p => p.PriorityIcon.ClassName = _priorityIconService.IconNameToClass(p.PriorityIcon.Name));

            return View(vm);
        }

        [HttpPost]
        [Route("Projects/{projectId}/CreateIssue")]
        public async Task<IActionResult> Create(int projectId, CreateIssueViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            };

            vm.ProjectId = projectId;
            vm.ReporterId = 5; // TODO: Remove this
            await _issueService.CreateIssueAsync(_mapper.Map<CreateIssueDTO>(vm));

            return View(vm);
        }
    }
}
