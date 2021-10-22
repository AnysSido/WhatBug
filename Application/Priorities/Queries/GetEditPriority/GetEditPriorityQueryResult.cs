using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Priorities.Queries.GetEditPriority
{
    public class GetEditPriorityQueryResult : IMapFrom<Priority>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public int Order { get; set; }
        public int IconId { get; set; }
        public int ColorId { get; set; }
        public IList<IconDTO> Icons { get; set; }
        public IList<ColorDTO> Colors { get; set; }
    }

    public class IconDTO : IMapFrom<Icon>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebName { get; set; }
    }

    public class ColorDTO : IMapFrom<Color>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}