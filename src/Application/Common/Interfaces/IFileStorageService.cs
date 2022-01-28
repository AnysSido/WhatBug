using System.Threading.Tasks;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IFileStorageService
    {
        string GetContentType(string fileId);
        string GetAttachmentPath(string fileId);
        Task SaveAttachmentAsync(byte[] file, string fileName);
    }
}
