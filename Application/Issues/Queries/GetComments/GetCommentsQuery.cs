using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Issues.Queries.GetComments
{
    public class GetCommentsQuery : IRequest<CommentsDTO>
    {
        public string IssueId { get; set; }
    }
}
