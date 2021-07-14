using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Entities;

namespace WhatBug.Domain.Data
{
    public class Icons
    {
        private static readonly List<Icon> _icons = new List<Icon>();

        public static readonly Icon ArrowUp = CreateIcon(1, "ArrowUp");
        public static readonly Icon ArrowDown = CreateIcon(2, "ArrowDown");
        public static readonly Icon ArrowLeft = CreateIcon(3, "ArrowLeft");
        public static readonly Icon ArrowRight = CreateIcon(4, "ArrowRight");
        public static readonly Icon CircleArrowUp = CreateIcon(5, "CircleArrowUp");
        public static readonly Icon CircleArrowDown = CreateIcon(6, "CircleArrowDown");
        public static readonly Icon CircleArrowLeft = CreateIcon(7, "CircleArrowLeft");
        public static readonly Icon CircleArrowRight = CreateIcon(8, "CircleArrowRight");
        public static readonly Icon ChevronUp = CreateIcon(9, "ChevronUp");
        public static readonly Icon ChevronDown = CreateIcon(10, "ChevronDown");
        public static readonly Icon ChevronLeft = CreateIcon(11, "ChevronLeft");
        public static readonly Icon ChevronRight = CreateIcon(12, "ChevronRight");
        public static readonly Icon AngleUp = CreateIcon(13, "AngleUp");
        public static readonly Icon AngleDown = CreateIcon(14, "AngleDown");
        public static readonly Icon AngleLeft = CreateIcon(15, "AngleLeft");
        public static readonly Icon AngleRight = CreateIcon(16, "AngleRight");
        public static readonly Icon AnglesUp = CreateIcon(17, "AnglesUp");
        public static readonly Icon AnglesDown = CreateIcon(18, "AnglesDown");
        public static readonly Icon AnglesLeft = CreateIcon(19, "AnglesLeft");
        public static readonly Icon AnglesRight = CreateIcon(20, "AnglesRight");
        public static readonly Icon Exclaimation = CreateIcon(21, "Exclaimation");
        public static readonly Icon CircleExclaimation = CreateIcon(22, "CircleExclaimation");
        public static readonly Icon TriangleExclaimation = CreateIcon(23, "TriangleExclaimation");
        public static readonly Icon XMark = CreateIcon(24, "XMark");
        public static readonly Icon Ban = CreateIcon(25, "Ban");
        public static readonly Icon EqualsSign = CreateIcon(26, "Equals");

        private static Icon CreateIcon(int id, string name)
        {
            var icon = new Icon { Id = id, Name = name };
            _icons.Add(icon);
            return icon;
        }

        public static ReadOnlyCollection<Icon> GetAll()
        {
            return _icons.AsReadOnly();
        }
    }
}
