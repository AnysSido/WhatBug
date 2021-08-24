using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Priorities.Commands.CreatePriority
{
    public class CreatePriorityCommandValidator : AbstractValidator<CreatePriorityCommand>
    {
        public CreatePriorityCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty();
            RuleFor(v => v.ColorId).NotEmpty();
            RuleFor(v => v.IconId).NotEmpty();
        }
    }
}
