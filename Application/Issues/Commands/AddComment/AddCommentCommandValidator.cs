using FluentValidation;

namespace WhatBug.Application.Issues.Commands.AddComment
{
    public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidator()
        {
            RuleFor(v => v.Content).NotEmpty();
            RuleFor(v => v.AuthorId).NotEmpty();
            RuleFor(v => v.IssueId).NotEmpty();
        }
    }
}
