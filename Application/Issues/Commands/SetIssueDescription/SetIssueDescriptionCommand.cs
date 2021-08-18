using MediatR;

namespace WhatBug.Application.Issues.Commands.SetIssueDescription
{
    public class SetIssueDescriptionCommand : IRequest
    {
        public string IssueId { get; set; }
        public string Description { get; set; }
    }
}
