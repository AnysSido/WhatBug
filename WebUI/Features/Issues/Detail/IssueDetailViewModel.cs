using WhatBug.Application.Issues.Queries.GetIssueDetail;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.Issues.Detail
{
    public class IssueDetailViewModel : IMapFrom<IssueDetailDTO>
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string PriorityName { get; set; }
        public string PriorityIconName { get; set; }
        public string PriorityIconColor { get; set; }
    }
}
