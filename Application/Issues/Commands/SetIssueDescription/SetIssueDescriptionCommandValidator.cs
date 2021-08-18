using FluentValidation;

namespace WhatBug.Application.Issues.Commands.SetIssueDescription
{
    public class SetIssueDescriptionCommandValidator : AbstractValidator<SetIssueDescriptionCommand>
    {
        public SetIssueDescriptionCommandValidator()
        {
            RuleFor(v => v.IssueId).NotEmpty();
        }
    }
}
