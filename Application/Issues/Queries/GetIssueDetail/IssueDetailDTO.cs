using AutoMapper;
using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Queries.GetIssueDetail
{
    public class IssueDetailDTO : IMapFrom<Issue>
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ProjectName { get; set; }

        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        public string PriorityIconName { get; set; }
        public string PriorityIconWebName { get; set; }
        public string PriorityIconColor { get; set; }

        public int IssueTypeId { get; set; }
        public string IssueTypeName { get; set; }
        public string IssueTypeIconName { get; set; }
        public string IssueTypeIconWebName { get; set; }
        public string IssueTypeIconColor { get; set; }

        public string AssigneeFirstName { get; set; }
        public string AssigneeSurname { get; set; }
        public string AssigneeEmail { get; set; }

        public string ReporterFirstName { get; set; }
        public string ReporterSurname { get; set; }
        public string ReporterEmail { get; set; }

        public int AttachmentCount { get; set; }

        public IList<IssueTypeDTO> IssueTypes { get; set; }
        public IList<PriorityDTO> Priorities { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Issue, IssueDetailDTO>()
                .ForMember(d => d.PriorityIconColor, opt => opt.MapFrom(s => s.Priority.Color.Name))
                .ForMember(d => d.IssueTypeIconColor, opt => opt.MapFrom(s => s.IssueType.Color.Name))
                .ForMember(d => d.AttachmentCount, opt => opt.MapFrom(s => s.Attachments.Count));
        }
    }

    public class IssueTypeDTO : IMapFrom<IssueType>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconWebName { get; set; }
        public string ColorName { get; set; }
    }

    public class PriorityDTO : IMapFrom<Priority>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconWebName { get; set; }
        public string ColorName { get; set; }
        public int Order { get; set; }
        public bool IsDefault { get; set; }
    }
}