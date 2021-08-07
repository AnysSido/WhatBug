﻿using System.Collections.Generic;
using WhatBug.WebUI.ViewModels.Projects;

namespace WhatBug.WebUI.ViewComponents
{
    public class DefaultNavigationComponentViewModel
    {
        public IList<ProjectViewModel> Projects { get; set; } = new List<ProjectViewModel>();
    }
}
