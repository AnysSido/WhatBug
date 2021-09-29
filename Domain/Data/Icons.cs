using System.Collections.Generic;
using System.Collections.ObjectModel;
using WhatBug.Domain.Entities;

namespace WhatBug.Domain.Data
{
    /*
     *  Internal icon names used by WhatBug to allow users to assign icons to priorities etc.
     *  The web names must be mapped in css to actual icons (e.g. font-awesome).
     */

    public class Icons
    {
        private static readonly List<Icon> _icons = new List<Icon>();

        public static readonly Icon ArrowUp = CreateIcon(1, "ArrowUp", "arrow-up");
        public static readonly Icon ArrowDown = CreateIcon(2, "ArrowDown", "arrow-down");
        public static readonly Icon ArrowLeft = CreateIcon(3, "ArrowLeft", "arrow-left");
        public static readonly Icon ArrowRight = CreateIcon(4, "ArrowRight", "arrow-right");
        public static readonly Icon CircleArrowUp = CreateIcon(5, "CircleArrowUp", "arrow-circle-up");
        public static readonly Icon CircleArrowDown = CreateIcon(6, "CircleArrowDown", "arrow-circle-down");
        public static readonly Icon CircleArrowLeft = CreateIcon(7, "CircleArrowLeft", "arrow-circle-left");
        public static readonly Icon CircleArrowRight = CreateIcon(8, "CircleArrowRight", "arrow-circle-right");
        public static readonly Icon ChevronUp = CreateIcon(9, "ChevronUp", "chevron-up");
        public static readonly Icon ChevronDown = CreateIcon(10, "ChevronDown", "chevron-down");
        public static readonly Icon ChevronLeft = CreateIcon(11, "ChevronLeft", "chevron-left");
        public static readonly Icon ChevronRight = CreateIcon(12, "ChevronRight", "chevron-right");
        public static readonly Icon AngleUp = CreateIcon(13, "AngleUp", "angle-up");
        public static readonly Icon AngleDown = CreateIcon(14, "AngleDown", "angle-down");
        public static readonly Icon AngleLeft = CreateIcon(15, "AngleLeft", "angle-left");
        public static readonly Icon AngleRight = CreateIcon(16, "AngleRight", "angle-right");
        public static readonly Icon AnglesUp = CreateIcon(17, "AnglesUp", "angles-up");
        public static readonly Icon AnglesDown = CreateIcon(18, "AnglesDown", "angles-down");
        public static readonly Icon AnglesLeft = CreateIcon(19, "AnglesLeft", "angles-left");
        public static readonly Icon AnglesRight = CreateIcon(20, "AnglesRight", "angles-right");
        public static readonly Icon Exclaimation = CreateIcon(21, "Exclaimaton", "exclamation");
        public static readonly Icon CircleExclaimation = CreateIcon(22, "Circle Exclamation", "exclamation-circle");
        public static readonly Icon TriangleExclaimation = CreateIcon(23, "Triangle Exclamation", "exclamation-triangle");
        public static readonly Icon XMark = CreateIcon(24, "X Mark", "x-mark");
        public static readonly Icon Ban = CreateIcon(25, "Ban", "ban");
        public static readonly Icon EqualsSign = CreateIcon(26, "Equals", "equals");
        public static readonly Icon Bug = CreateIcon(27, "Bug", "bug");
        public static readonly Icon PlusSquare = CreateIcon(28, "Square Plus", "plus-square");
        public static readonly Icon CheckSquare = CreateIcon(29, "Square Check", "check-square");
        public static readonly Icon CaretSquareUp = CreateIcon(30, "Square Caret Up", "caret-up-square");
        public static readonly Icon CaretSquareDown = CreateIcon(31, "Square Caret Down", "caret-down-square");
        public static readonly Icon CaretSquareLeft = CreateIcon(32, "Square Caret Left", "caret-left-square");
        public static readonly Icon CaretSquareRight = CreateIcon(33, "Square Caret Right", "caret-right-square");
        public static readonly Icon Pen = CreateIcon(34, "Pen", "pen");
        public static readonly Icon Plus = CreateIcon(35, "Plus", "plus");
        public static readonly Icon Information = CreateIcon(36, "Information", "information");

        private static Icon CreateIcon(int id, string name, string webName)
        {
            var icon = new Icon { Id = id, Name = name, WebName = webName };
            _icons.Add(icon);
            return icon;
        }

        public static ReadOnlyCollection<Icon> Seed()
        {
            return _icons.AsReadOnly();
        }
    }
}