using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Services.Interfaces
{
    public interface IIconService
    {
        string IconNameToClass(string iconName);
        string ClassToIconName(string className);
    }
}
