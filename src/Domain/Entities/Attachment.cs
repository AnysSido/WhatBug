using System;

namespace WhatBug.Domain.Entities
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }

        public string IssueId { get; set; }
        public Issue Issue { get; set; }
    }
}
