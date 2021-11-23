﻿using System.Collections.Generic;

namespace WhatBug.Application.Common.Settings
{
    public class WhatBugSettings
    {
        public AccountSettings Accounts { get; set; } = new AccountSettings();
        public AttachmentSettings Attachments { get; set; } = new AttachmentSettings();
    }

    public class AccountSettings
    {
        public bool RegistrationEnabled;
    }

    public class AttachmentSettings
    {
        public string FileLocation { get; set; }
        public List<string> AllowedExtensions { get; set; } = new List<string>();
        public long MaxFileSize; // Size in MB
    }
}
