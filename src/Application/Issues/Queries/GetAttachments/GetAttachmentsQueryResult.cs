using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Queries.GetAttachments
{
    public class GetAttachmentsQueryResult
    {
        public List<AttachmentDTO> Attachments { get; set; }
    }

    public class AttachmentDTO : IMapFrom<Attachment>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
    }
}