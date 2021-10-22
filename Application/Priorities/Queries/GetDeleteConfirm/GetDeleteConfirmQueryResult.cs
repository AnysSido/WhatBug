using AutoMapper;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Priorities.Queries.GetDeleteConfirm
{
    public class GetDeleteConfirmQueryResult : IMapFrom<Priority>
    {
        public int PriorityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Priority, GetDeleteConfirmQueryResult>()
                .ForMember(d => d.PriorityId, opt => opt.MapFrom(s => s.Id));
        }
    }
}