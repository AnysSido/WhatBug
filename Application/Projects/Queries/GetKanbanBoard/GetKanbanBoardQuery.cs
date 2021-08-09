using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Projects.Queries.GetKanbanBoard
{
    public class GetKanbanBoardQuery : IRequest<KanbanBoardDTO>
    {
        public int ProjectId { get; set; }
    }
}
