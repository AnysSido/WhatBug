using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Issues.Queries.GetIssueDetail
{
    public class GetIssueDetailQuery : IRequest<IssueDetailDTO>
    {
        public int Id { get; set; }
    }
}
