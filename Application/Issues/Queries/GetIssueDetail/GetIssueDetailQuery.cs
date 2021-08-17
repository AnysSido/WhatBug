using MediatR;

namespace WhatBug.Application.Issues.Queries.GetIssueDetail
{
    public class GetIssueDetailQuery : IRequest<IssueDetailDTO>
    {
        public string Id { get; set; }
    }
}
