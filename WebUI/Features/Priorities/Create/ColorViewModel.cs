using WhatBug.Application.Priorities.Queries.GetCreatePriority;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.Priorities.Create
{
    public class ColorViewModel : IMapFrom<ColorDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LowerName => Name.ToLowerInvariant();
    }
}