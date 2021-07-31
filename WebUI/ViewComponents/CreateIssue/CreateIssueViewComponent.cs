using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Issues;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.ViewComponents
{
    public class CreateIssueViewComponent : ViewComponent
    {
        private readonly IProjectService _projectService;
        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;

        public CreateIssueViewComponent(IProjectService projectService, IIssueService issueService, IMapper mapper)
        {
            _projectService = projectService;
            _issueService = issueService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var project = await _projectService.GetProjectAsync(8);
            var vm = new CreateIssueComponentViewModel()
            {
                AllSchemePriorities = _mapper.Map<List<PriorityViewModel>>(project.PriorityScheme.Priorities),
                AllIssueTypes = _mapper.Map<List<IssueTypeViewModel>>(await _issueService.GetIssueTypesAsync())
            };

            return View(vm);
        }
    }
}
