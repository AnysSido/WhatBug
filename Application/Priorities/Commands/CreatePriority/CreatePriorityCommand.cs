using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Priorities.Commands.CreatePriority
{
    public class CreatePriorityCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColorId { get; set; }
        public int IconId { get; set; }
    }
}
