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

        public ProjectNavigationViewComponent(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int projectId)
        {
            var project = await _projectService.GetProjectAsync(projectId);
            var vm = new ProjectNavigationComponentViewModel
            {
                ProjectId = project.Id,
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
