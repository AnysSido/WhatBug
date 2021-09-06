using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Issues.Commands.AttachFile;
using WhatBug.Application.Issues.Queries.GetAttachments;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.Issues.Attachments
{
    public class AttachmentsController : BaseController
    {
        private readonly IMediator _mediatr;
        private readonly IFileStorageService _fileStorageService;

        public AttachmentsController(IMediator mediatr, IFileStorageService fileStorageService)
        {
            _mediatr = mediatr;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        [Route("{controller}/get/{fileId}")]
        public async Task<IActionResult> Get(string fileId)
        {
            var path = _fileStorageService.GetAttachmentPath(fileId);
            var contentType = _fileStorageService.GetContentType(fileId);

            return PhysicalFile(path, contentType);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string issueId, IFormFile file)
        {
            // Validate the file
            // Verify the file

            var fileName = file.FileName.Trim('"');


            // Convert the file
            byte[] buffer = new byte[file.Length];
            using (var stream = file.OpenReadStream())
            {
                stream.Read(buffer);
            }

            var command = new AttachFileCommand
            {
                IssueId = issueId,
                FileName = fileName,
                File = buffer,
                ContentType = file.ContentType
            };

            await _mediatr.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAttachments(string issueId)
        {
            var attachments = await Mediator.Send(new GetAttachmentsQuery { IssueId = issueId });

            return Json(JsonSerializer.Serialize(attachments.Attachments));
        }
    }
}
