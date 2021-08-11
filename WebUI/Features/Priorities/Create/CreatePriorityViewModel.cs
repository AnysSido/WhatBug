using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Priorities.Queries.GetCreatePriority;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.Priorities.Create
{
    public class CreatePriorityViewModel : IMapFrom<CreatePriorityDTO>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int IconId { get; set; }
        public int ColorId { get; set; }
        public IList<IconViewModel> Icons { get; set; }
        public IList<ColorViewModel> Colors { get; set; }
    }
}
