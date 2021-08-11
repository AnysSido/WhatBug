using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes
{
    public class PrioritySchemeDTO : IMapFrom<PriorityScheme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<PriorityDTO> Priorities { get; set; }
    }
}
