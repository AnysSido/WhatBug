using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Services.Interfaces
{
    public interface IPriorityIconService
    {
        string IconNameToClass(string iconName);
        string ClassToIconName(string className);
    }
}
