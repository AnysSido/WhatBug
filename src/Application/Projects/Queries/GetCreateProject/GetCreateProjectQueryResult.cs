using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetCreateProject
{
    public class GetCreateProjectQueryResult
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public int PrioritySchemeId { get; set; }
        public int PermissionSchemeId { get; set; }
        public IList<PrioritySchemeDto> PrioritySchemes { get; set; }
        public IList<PermissionSchemeDto> PermissionSchemes { get; set; }
    }
    public class PrioritySchemeDto : IMapFrom<PriorityScheme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }

    public class PermissionSchemeDto : IMapFrom<PermissionScheme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
