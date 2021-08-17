using MediatR;

namespace WhatBug.Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public int PrioritySchemeId { get; set; }
    }
}
