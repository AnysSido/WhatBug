using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.PrioritySchemes.Create
{
    public class CreatePrioritySchemeViewModel : IMapFrom<CreatePrioritySchemeDTO>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<int> PriorityIds { get; set; }
        public IList<PriorityViewModel> Priorities { get; set; }

        public IEnumerable<SelectListItem> PriorityListItems => Priorities.Select(p =>
            new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }
        );
    }
}
