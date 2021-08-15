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
    public class CreateIssueOldViewComponent : ViewComponent
    {
        private readonly IProjectService _projectService;
        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;

        public CreateIssueOldViewComponent(IProjectService projectService, IIssueService issueService, IMapper mapper)
        {
            _projectService = projectService;
            _issueService = issueService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(CreateIssueComponentViewModel vm)
        {
            var projects = await _projectService.GetAllProjects();

            vm ??= new CreateIssueComponentViewModel();
            vm.AllProjects = _mapper.Map<List<ProjectViewModel>>(projects);
            vm.AllIssueTypes = _mapper.Map<List<IssueTypeViewModel>>(await _issueService.GetIssueTypesAsync());            

            //var selectedProject = projects[0];

            // TODO: Default to the current project if this component is opened from a project window

            //if (Url.TryGetRouteInt("projectId", out int projectId))
            //{
            //    selectedProject = projects.First(p => p.Id == projectId);
            //    projects.Remove(selectedProject);
            //    projects.Insert(0, selectedProject);
                
            //}

            //if (vm.SelectedProjectId == 0)
            //    vm.SelectedProjectId = projects.First().Id;

            return View(vm);
        }
    }
}
