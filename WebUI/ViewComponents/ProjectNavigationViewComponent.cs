using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Services.Interfaces;

namespace WhatBug.WebUI.ViewComponents
{
    public class ProjectNavigationViewComponent : ViewComponent
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectNavigationViewComponent(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int projectId)
        {
            var project = await _projectService.GetProjectAsync(projectId);
            var vm = new ProjectNavigationComponentViewModel
            {
                ProjectId = projectId,
                ProjectName = project.Name
            };

            return View(vm);
        }
    }

    public class ProjectNavigationComponentViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}
