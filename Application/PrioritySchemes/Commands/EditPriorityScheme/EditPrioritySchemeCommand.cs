using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.PrioritySchemes.Commands.EditPriorityScheme
{
    public class EditPrioritySchemeCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> PriorityIds { get; set; }
    }
}
