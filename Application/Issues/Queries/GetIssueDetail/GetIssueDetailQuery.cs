using MediatR;

namespace WhatBug.Application.Issues.Queries.GetIssueDetail
{
    public class GetIssueDetailQuery : IRequest<IssueDetailDTO>
    {
        public string IssueId { get; set; }
    }
}
