using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.PrioritySchemes.Create
{
    public class PriorityViewModel : IMapFrom<PriorityDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
