using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Result;
using WhatBug.Application.Common.Settings;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Commands.AttachFile
{
    public class AttachFileCommandHandler : IRequestHandler<AttachFileCommand, Result>
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

        public async Task<Result> Handle(AttachFileCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions

            var fileSize = request.File.Length;
            var maxFileSize = _whatbugSettings.Attachments.MaxFileSize * 1024 * 1024;

            if (fileSize > maxFileSize)
                return Result.Failure(Errors.Issues.AttachmentTooBig(request.FileName, maxFileSize));

            var extension = Path.GetExtension(request.FileName);

            if (!_whatbugSettings.Attachments.AllowedExtensions.Contains(extension))
                return Result.Failure(Errors.Issues.FileTypeNotAllowed(extension));

            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == request.IssueId, cancellationToken);
            if (issue == null)
                return Result.Failure(Errors.Issues.IssueNotFound(request.IssueId));

            var fileName = Path.GetFileName(request.FileName);

            var attachment = new Attachment
            {
                Issue = issue,
                FileName = $"{Guid.NewGuid()}{extension}",
                OriginalFileName = fileName,
                FileSize = fileSize,
                ContentType = request.ContentType,
            };

            await _context.Attachments.AddAsync(attachment);

            // Store the file
            await _fileStorageService.SaveAttachmentAsync(request.File, attachment.FileName);

            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
