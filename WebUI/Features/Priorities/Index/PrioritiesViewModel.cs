using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Priorities.Queries.GetPriorities;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.Priorities.Index
{
    public class PrioritiesViewModel : IMapFrom<PrioritiesDTO>
    {
        public IList<PriorityViewModel> Priorities { get; set; }
    }
}
