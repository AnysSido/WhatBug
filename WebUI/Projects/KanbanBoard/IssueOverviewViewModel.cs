using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Queries.GetKanbanBoard;
using WhatBug.Common.Mapping;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Projects.KanbanBoard
{
    public class IssueOverviewViewModel : IMapFrom<IssueOverviewDTO>
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int IssueStatusId { get; set; }
        public string IssueStatusName { get; set; }
        public string IssueTypeIconColor { get; set; }
        public string IssueTypeIconName { get; set; }
        public string PriorityIconColor { get; set; }
        public string PriorityIconName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IssueOverviewDTO, IssueOverviewViewModel>()
                .ForMember(d => d.IssueTypeIconName, opt => opt.MapFrom<IconClassNameResolver, string>(s => s.IssueTypeIconName))
                .ForMember(d => d.IssueTypeIconColor, opt => opt.MapFrom(s => s.IssueTypeIconColor.ToLowerInvariant()))
                .ForMember(d => d.PriorityIconName, opt => opt.MapFrom<IconClassNameResolver, string>(s => s.PriorityIconName))
                .ForMember(d => d.PriorityIconColor, opt => opt.MapFrom(s => s.PriorityIconColor.ToLowerInvariant()));
        }
    }
}
