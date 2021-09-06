using FluentValidation;

namespace WhatBug.Application.Issues.Queries.GetAttachments
{
    public class GetAttachmentsQueryValidator : AbstractValidator<GetAttachmentsQuery>
    {
        public GetAttachmentsQueryValidator()
        {
            RuleFor(v => v.IssueId).NotEmpty();
        }
    }
}
