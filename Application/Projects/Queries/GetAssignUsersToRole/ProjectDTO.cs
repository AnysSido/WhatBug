using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetAssignUsersToRole
{
    public class ProjectDTO : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}