using FluentValidation;

namespace WhatBug.Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty();
            RuleFor(v => v.PrioritySchemeId).NotEmpty();
            RuleFor(v => v.Key).NotEmpty().Length(2, 10).Matches("^[A-Z]+$");
        }
    }
}
