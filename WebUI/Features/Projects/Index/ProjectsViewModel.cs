using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Queries.GetProjects;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.Projects.Index
{
    public class ProjectsViewModel : IMapFrom<ProjectsDTO>
    {
        public IList<ProjectViewModel> Projects { get; set; }
    }
}
