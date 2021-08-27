using System.Collections.Generic;

namespace WhatBug.Application.Issues.Queries.GetComments
{
    public class CommentsDTO
    {
        public IList<CommentDTO> Comments { get; set; }
    }
}