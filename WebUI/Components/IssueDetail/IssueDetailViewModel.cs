using WhatBug.Application.Issues.Queries.GetIssueDetail;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Components.IssueDetail
{
    public class IssueDetailViewModel : IMapFrom<IssueDetailDTO>
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ProjectName { get; set; }

        public string PriorityName { get; set; }
        public string PriorityIconName { get; set; }
        public string PriorityIconColor { get; set; }

        public string IssueTypeName { get; set; }
        public string IssueTypeIconName { get; set; }
        public string IssueTypeIconColor { get; set; }

        public string AssigneeFirstName { get; set; }
        public string AssigneeSurname { get; set; }
        public string AssigneeEmail { get; set; }
        public string AssigneeFullName => $"{AssigneeFirstName} {AssigneeSurname}";

        public string ReporterFirstName { get; set; }
        public string ReporterSurname { get; set; }
        public string ReporterEmail { get; set; }
        public string ReporterFullName => $"{ReporterFirstName} {ReporterSurname}";
    }
}
