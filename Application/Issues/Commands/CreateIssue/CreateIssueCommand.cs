using MediatR;
using WhatBug.Application.Common.Result;

namespace WhatBug.Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommand : IRequest<Result>
    {
        public int ProjectId { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public int IssueTypeId { get; set; }
        public int? AssigneeId { get; set; }
        public int ReporterId { get; set; }
    }
}
