using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Navigation.Queries.GetProjectNavigation
{
    public class ProjectNavigationDTO : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
