using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Issues.Queries.GetIssueDetail
{
    public class GetIssueDetailQueryValidator : AbstractValidator<GetIssueDetailQuery>
    {
        public GetIssueDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
