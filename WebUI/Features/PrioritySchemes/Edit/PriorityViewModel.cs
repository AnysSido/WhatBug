using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.PrioritySchemes.Edit
{
    public class PriorityViewModel : IMapFrom<PriorityDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
