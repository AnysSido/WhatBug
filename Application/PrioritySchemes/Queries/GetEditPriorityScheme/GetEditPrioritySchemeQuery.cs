using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme
{
    public class GetEditPrioritySchemeQuery : IRequest<EditPrioritySchemeDTO>
    {
        public int Id { get; set; }
    }
}
