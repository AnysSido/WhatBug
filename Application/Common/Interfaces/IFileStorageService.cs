using System.Threading.Tasks;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IFileStorageService
    {
        string GetContentType(string fileId);
        string GetAttachmentPath(string fileId);
        Task<bool> SaveAttachmentAsync(byte[] file, string fileName);
    }
}
