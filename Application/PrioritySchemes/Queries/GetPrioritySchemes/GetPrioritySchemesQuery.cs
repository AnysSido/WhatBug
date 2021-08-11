using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes
{
    public class GetPrioritySchemesQuery : IRequest<PrioritySchemesDTO>
    {
    }
}
