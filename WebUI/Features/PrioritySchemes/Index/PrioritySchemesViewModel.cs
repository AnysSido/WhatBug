using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.PrioritySchemes.Index
{
    public class PrioritySchemesViewModel : IMapFrom<PrioritySchemesDTO>
    {
        public IList<PrioritySchemeViewModel> PrioritySchemes { get; set; }
    }
}
