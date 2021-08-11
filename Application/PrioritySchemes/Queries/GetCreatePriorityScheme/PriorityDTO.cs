using AutoMapper;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme
{
    public class PriorityDTO : IMapFrom<Priority>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}