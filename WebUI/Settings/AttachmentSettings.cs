using System.Collections.Generic;

namespace WhatBug.WebUI.Settings
{
    public class AttachmentSettings
    {
        public string FileLocation { get; set; } = "\\userfiles\\attachments";
        public List<string> AllowedExtensions { get; set; } = new List<string> { ".gif", ".jpg", ".jpeg", ".png", ".svg" };
        public long MaxFileSize = 1; // Size in MB
    }
}
