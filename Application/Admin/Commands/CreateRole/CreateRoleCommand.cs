using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Result;

namespace WhatBug.Application.Admin.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
