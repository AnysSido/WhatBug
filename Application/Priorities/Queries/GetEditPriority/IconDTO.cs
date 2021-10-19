using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Priorities.Queries.GetEditPriority
{
    public class IconDTO : IMapFrom<Icon>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebName { get; set; }
    }
}