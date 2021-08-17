using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Priorities.Queries.GetEditPriority
{
    public class GetEditPriorityQueryValidator : AbstractValidator<GetEditPriorityQuery>
    {
        public GetEditPriorityQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
