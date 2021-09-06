using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Settings;

namespace WhatBug.WebUI.Services
{
    public class FileSystemFileStorageService : IFileStorageService
    {
        private readonly string _rootPath;
        private readonly string _attachmentDir;

        public FileSystemFileStorageService(IWebHostEnvironment env, IOptions<WhatBugSettings> appSettings)
        {
            _rootPath = env.ContentRootPath;
            _attachmentDir = appSettings.Value.Attachments.FileLocation;
        }

        public async Task<bool> SaveAttachmentAsync(byte[] file, string fileName)
        {
            var dir = Path.Combine(_rootPath, _attachmentDir);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var filePath = Path.Combine(dir, fileName);

            await File.WriteAllBytesAsync(filePath, file);

            return true;
        }

        public string GetAttachmentPath(string fileId)
        {
            return Path.Combine(_rootPath, _attachmentDir, fileId);
        }

        public string GetContentType(string fileId)
        {
            var fileProvider = new FileExtensionContentTypeProvider();
            if (fileProvider.TryGetContentType(fileId, out string contentType))
            {
                return contentType;
            }

            return string.Empty;
        }
    }
}
