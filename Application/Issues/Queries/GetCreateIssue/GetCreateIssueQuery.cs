using MediatR;

namespace WhatBug.Application.Issues.Queries.GetCreateIssue
{
    public class GetCreateIssueQuery :IRequest<CreateIssueDTO>
    {
        public int? ProjectId { get; set; }
    }
}
