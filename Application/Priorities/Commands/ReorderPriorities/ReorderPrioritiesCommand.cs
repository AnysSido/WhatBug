using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Priorities.Commands.ReorderPriorities
{
    public class ReorderPrioritiesCommand : IRequest
    {
        public IList<int> Ids { get; set; }
    }
}
