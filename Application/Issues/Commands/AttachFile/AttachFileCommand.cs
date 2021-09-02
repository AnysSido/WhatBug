using MediatR;
using System;
using WhatBug.Application.Common.Result;

namespace WhatBug.Application.Issues.Commands.AttachFile
{
    public class AttachFileCommand : IRequest<Result>
    {
        public string IssueId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] File { get; set; }
    }
}
