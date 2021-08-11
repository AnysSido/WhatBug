using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Priorities.Queries.GetCreatePriority
{
    public class IconDTO : IMapFrom<Icon>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}