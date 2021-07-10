using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.Services.Interfaces;

namespace WhatBug.WebUI.Services
{
    public class FontAwesomePriorityIconService : IPriorityIconService
    {
        private string _iconClassPrefix = "fas fa-fw fa-";
        private string _default = "times";
        private readonly Dictionary<string, string> _iconMap = new Dictionary<string, string>()
        {
            {"ArrowUp", "arrow-up" },
            {"ArrowDown", "arrow-down" },
            {"ArrowLeft", "arrow-left" },
            {"ArrowRight", "arrow-right" },
            {"CircleArrowUp", "arrow-circle-up" },
            {"CircleArrowDown", "arrow-circle-down" },
            {"CircleArrowLeft", "arrow-circle-left" },
            {"CircleArrowRight", "arrow-circle-right" },
            {"ChevronUp", "chevron-up" },
            {"ChevronDown", "chevron-down" },
            {"ChevronLeft", "chevron-left" },
            {"ChevronRight", "chevron-right" },
            {"AngleUp", "angle-up" },
            {"AngleDown", "angle-down" },
            {"AngleLeft", "angle-left" },
            {"AngleRight", "angle-right" },
            {"AnglesUp", "angle-double-up" },
            {"AnglesDown", "angle-double-down" },
            {"AnglesLeft", "angle-double-left" },
            {"AnglesRight", "angle-double-right" },
            {"Exclaimation", "exclamation" },
            {"CircleExclaimation", "exclamation-circle" },
            {"TriangleExclaimation", "exclamation-triangle" },
            {"XMark", "times" },
            {"Ban", "ban" },
            {"Equals", "equals" },
        };

        public string IconNameToClass(string name)
        {
            return _iconMap.ContainsKey(name) ? _iconClassPrefix + _iconMap[name] : _iconClassPrefix + _default;
        }

        public string ClassToIconName(string className)
        {
            var iconClass = className.Substring(_iconClassPrefix.Length);
            return _iconMap.ContainsValue(iconClass) ? _iconMap.FirstOrDefault(x => x.Value == iconClass).Key : string.Empty;
        }
    }
}
