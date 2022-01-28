using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme
{
    public class CreatePrioritySchemeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<int> PriorityIds { get; set; }
        public IList<PriorityDTO> Priorities { get; set; }
    }

    public class PriorityDTO : IMapFrom<Priority>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconWebName { get; set; }
        public string ColorName { get; set; }
    }
}