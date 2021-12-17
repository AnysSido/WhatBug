using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Application.Common.Settings;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Commands.AttachFile
{
    [Authorize(Permissions.AttachFiles)]
    public record AttachFileCommand : ICommand<Response>, IRequireIssueAuthorization
    {
        public string IssueId { get; init; }
        public string FileName { get; init; }
        public string ContentType { get; init; }
        public byte[] File { get; init; }
    }

    public class AttachFileCommandHandler : IRequestHandler<AttachFileCommand, Response>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly WhatBugSettings _whatbugSettings;

        public AttachFileCommandHandler(IWhatBugDbContext context, IFileStorageService fileStorageService, IOptions<WhatBugSettings> whatbugSettings)
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _whatbugSettings = whatbugSettings.Value;
        }

        public async Task<Response> Handle(AttachFileCommand request, CancellationToken cancellationToken)
        {
            var fileSize = request.File.Length;
            var maxFileSize = _whatbugSettings.Attachments.MaxFileSize * 1024 * 1024;

            if (fileSize > maxFileSize)
                throw new InvalidAttachmentException($"File exceeds maximum allowed size ({_whatbugSettings.Attachments.MaxFileSize}MB).");

            var extension = Path.GetExtension(request.FileName);

            if (!_whatbugSettings.Attachments.AllowedExtensions.Contains(extension))
                throw new InvalidAttachmentException($"Attachment extension {extension} is not allowed.");

            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == request.IssueId, cancellationToken);

            var fileName = Path.GetFileName(request.FileName);

            var attachment = new Attachment
            {
                Issue = issue,
                FileName = $"{Guid.NewGuid()}{extension}",
                OriginalFileName = fileName,
                FileSize = fileSize,
                ContentType = request.ContentType,
            };

            _context.Attachments.Add(attachment);

            await _fileStorageService.SaveAttachmentAsync(request.File, attachment.FileName);

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}
