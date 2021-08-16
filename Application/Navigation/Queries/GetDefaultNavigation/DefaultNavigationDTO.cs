using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Navigation.Queries.GetDefaultNavigation
{
    public class DefaultNavigationDTO
    {
        public IList<ProjectDTO> Projects { get; set; }

        public class ProjectDTO : IMapFrom<Project>
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
