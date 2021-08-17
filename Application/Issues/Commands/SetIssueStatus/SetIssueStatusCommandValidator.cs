using FluentValidation;

namespace WhatBug.Application.Issues.Commands.SetIssueStatus
{
    public class SetIssueStatusCommandValidator : AbstractValidator<SetIssueStatusCommand>
    {
        public SetIssueStatusCommandValidator()
        {
            RuleFor(v => v.IssueId).NotEmpty();
            RuleFor(v => v.IssueStatusId).NotEmpty();
        }
    }
}
