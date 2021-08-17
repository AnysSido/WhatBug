using MediatR;

namespace WhatBug.Application.Issues.Commands.SetIssueStatus
{
    public class SetIssueStatusCommand : IRequest
    {
        public string IssueId { get; set; }
        public int IssueStatusId { get; set; }
    }
}
