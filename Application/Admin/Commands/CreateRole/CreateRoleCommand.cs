using MediatR;
using WhatBug.Application.Common.Models;

namespace WhatBug.Application.Admin.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<Response>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
