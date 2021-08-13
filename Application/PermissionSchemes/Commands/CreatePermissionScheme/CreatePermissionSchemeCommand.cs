using MediatR;

namespace WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme
{
    public class CreatePermissionSchemeCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
