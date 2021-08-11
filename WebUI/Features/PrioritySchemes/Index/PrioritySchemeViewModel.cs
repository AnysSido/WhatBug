using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.PrioritySchemes.Index
{
    public class PrioritySchemeViewModel : IMapFrom<PrioritySchemeDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<PriorityViewModel> Priorities { get; set; }
    }
}
