using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatBug.Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PrioritySchemeId { get; set; }
    }
}
