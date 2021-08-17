using WhatBug.Application.Priorities.Queries.GetEditPriority;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.Priorities.Edit
{
    public class ColorViewModel : IMapFrom<ColorDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LowerName => Name.ToLowerInvariant();
    }
}