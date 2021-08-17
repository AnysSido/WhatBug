using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme
{
    public class GetEditPrioritySchemeQueryValidator : AbstractValidator<GetEditPrioritySchemeQuery>
    {
        public GetEditPrioritySchemeQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
