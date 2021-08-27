using FluentValidation;

namespace WhatBug.Application.Issues.Queries.GetComments
{
    public class GetCommentsQueryValidator : AbstractValidator<GetCommentsQuery>
    {
        public GetCommentsQueryValidator()
        {
            RuleFor(v => v.IssueId).NotEmpty();
        }
    }
}
