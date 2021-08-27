using MediatR;
using WhatBug.Application.Common.Result;

namespace WhatBug.Application.Issues.Commands.AddComment
{
    public class AddCommentCommand : IRequest<Result>
    {
        public string IssueId { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
    }
}
