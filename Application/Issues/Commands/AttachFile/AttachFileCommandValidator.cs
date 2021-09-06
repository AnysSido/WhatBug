using FluentValidation;

namespace WhatBug.Application.Issues.Commands.AttachFile
{
    public class AttachFileCommandValidator : AbstractValidator<AttachFileCommand>
    {
        public AttachFileCommandValidator()
        {
            RuleFor(v => v.FileName).NotEmpty();
            RuleFor(v => v.IssueId).NotEmpty();
            RuleFor(v => v.File).NotEmpty();
            RuleFor(v => v.ContentType).NotEmpty();
        }
    }
}
