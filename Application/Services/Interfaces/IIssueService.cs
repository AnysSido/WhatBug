using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IIssueService
    {
        public void CreateIssue(string name, string description);
    }
}
