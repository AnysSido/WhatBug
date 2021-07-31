using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Issues;
using WhatBug.WebUI.ViewModels.Priorities;
using WhatBug.WebUI.ViewModels.Projects;

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
            var projects = await _projectService.GetAllProjects();

            var vm = new CreateIssueComponentViewModel()
            {
                AllProjects = _mapper.Map<List<ProjectViewModel>>(projects),
                AllIssueTypes = _mapper.Map<List<IssueTypeViewModel>>(await _issueService.GetIssueTypesAsync())
            };

            var selectedProject = projects[0];

            // TODO: Default to the current project if this component is opened from a project window

            //if (Url.TryGetRouteInt("projectId", out int projectId))
            //{
            //    selectedProject = projects.First(p => p.Id == projectId);
            //    projects.Remove(selectedProject);
            //    projects.Insert(0, selectedProject);
                
            //}

            vm.SelectedProjectId = selectedProject.Id;
            vm.AllSchemePriorities = _mapper.Map<List<PriorityViewModel>>(selectedProject.PriorityScheme.Priorities);

            return View(vm);
        }
    }
}
