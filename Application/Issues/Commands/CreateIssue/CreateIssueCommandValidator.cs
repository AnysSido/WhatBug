using FluentValidation;

namespace WhatBug.Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommandValidator : AbstractValidator<CreateIssueCommand>
    {
        public CreateIssueCommandValidator()
        {
            RuleFor(v => v.Summary).NotEmpty();
            RuleFor(v => v.ProjectId).NotEmpty();
            RuleFor(v => v.PriorityId).NotEmpty();
            RuleFor(v => v.IssueTypeId).NotEmpty();
            RuleFor(v => v.ReporterId).NotEmpty();
        }
    }
}
