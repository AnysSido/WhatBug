using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Projects.Queries.GetCreateProject
{
    public class GetCreateProjectQuery : IRequest<CreateProjectDTO>
    {
    }
}
