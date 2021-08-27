using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Issues.Queries.GetComments;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Components.IssueDetail
{
    public class CommentViewModel : IMapFrom<CommentDTO>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public UserDTO Author { get; set; }
        public bool IsByCurrentUser { get; set; }
        public DateTime Timestamp { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CommentDTO, CommentViewModel>()
                .AfterMap<SetIsByCurrentUserAction>();
        }

        private class SetIsByCurrentUserAction : IMappingAction<CommentDTO, CommentViewModel>
        {
            private readonly ICurrentUserService _currentUserService;

            public SetIsByCurrentUserAction(ICurrentUserService currentUserService)
            {
                _currentUserService = currentUserService;
            }

            public void Process(CommentDTO source, CommentViewModel destination, ResolutionContext context)
            {
                destination.IsByCurrentUser = destination.Author.Id == _currentUserService.Id;
            }
        }
    }
}