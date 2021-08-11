using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.PrioritySchemes.Commands.CreatePriorityScheme
{
    public class CreatePrioritySchemeCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> PriorityIds { get; set; }
    }
}
