using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Issues.Queries.GetIssueDetail;
using WhatBug.Common.Mapping;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.Issues.Detail
{
    public class IssueDetailViewModel : IMapFrom<IssueDetailDTO>
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string PriorityName { get; set; }
        public string PriorityIconClass { get; set; }
        public string PriorityIconColor { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IssueDetailDTO, IssueDetailViewModel>()
                .ForMember(d => d.PriorityIconClass, opt => opt.MapFrom<IconClassNameResolver, string>(s => s.PriorityIconName))
                .ForMember(d => d.PriorityIconColor, opt => opt.MapFrom(s => s.PriorityIconColor.ToLowerInvariant()));
        }
    }
}
