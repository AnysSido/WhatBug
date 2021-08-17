using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Queries.GetProjects;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.Projects.Index
{
    public class ProjectViewModel : IMapFrom<ProjectDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
