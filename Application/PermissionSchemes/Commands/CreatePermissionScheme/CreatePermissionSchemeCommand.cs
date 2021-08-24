using MediatR;
using WhatBug.Application.Common.Result;

namespace WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme
{
    public class CreatePermissionSchemeCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
