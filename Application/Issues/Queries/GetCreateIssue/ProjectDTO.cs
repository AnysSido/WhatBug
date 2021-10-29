using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Queries.GetCreateIssue
{
    public class ProjectDTO : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<PriorityDTO> Priorities { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectDTO>()
                .ForMember(d => d.Priorities, opt => opt.MapFrom(s => s.PriorityScheme.Priorities.Select(p => p.Priority)));
        }
    }
}