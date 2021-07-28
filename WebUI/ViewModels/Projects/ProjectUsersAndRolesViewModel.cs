using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.ViewModels.Projects
{
    public class ProjectUsersAndRolesViewModel
    {
        [HiddenInput]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public IList<ProjectRoleWithUsersViewModel> ProjectRolesWithUsers { get; set; }
    }
}
