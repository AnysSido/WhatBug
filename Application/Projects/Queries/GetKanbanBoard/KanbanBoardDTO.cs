using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetKanbanBoard
{
    public class KanbanBoardDTO : IMapFrom<Project>
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public IList<IssueStatusGroupDTO> IssueStatusGroups { get; set; } = new List<IssueStatusGroupDTO>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, KanbanBoardDTO>()
                .ForMember(d => d.ProjectName, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.ProjectDescription, opt => opt.MapFrom(s => s.Description));
        }
    }
}
