using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Projects.Commands.AssignsUsersToRole
{
    public class AssignUsersToRoleCommand : IRequest
    {
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<int> UserIds { get; set; }
    }
}
