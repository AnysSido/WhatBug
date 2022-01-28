using MediatR;

namespace WhatBug.Application.Navigation.Queries.GetProjectNavigation
{
    public class GetProjectNavigationQuery : IRequest<ProjectNavigationDTO>
    {
        public int ProjectId { get; set; }
    }
}
