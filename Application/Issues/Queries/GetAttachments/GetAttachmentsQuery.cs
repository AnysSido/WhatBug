using MediatR;

namespace WhatBug.Application.Issues.Queries.GetAttachments
{
    public class GetAttachmentsQuery : IRequest<AttachmentsDTO>
    {
        public string IssueId { get; set; }
    }
}
