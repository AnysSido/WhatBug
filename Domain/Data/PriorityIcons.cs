using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Domain.Data
{
    public class PriorityIcons
    {
        private static readonly List<PriorityIcon> _icons = new List<PriorityIcon>();

        public static readonly PriorityIcon ArrowUp = CreateIcon(1, "ArrowUp");
        public static readonly PriorityIcon ArrowDown = CreateIcon(2, "ArrowDown");
        public static readonly PriorityIcon ArrowLeft = CreateIcon(3, "ArrowLeft");
        public static readonly PriorityIcon ArrowRight = CreateIcon(4, "ArrowRight");
        public static readonly PriorityIcon CircleArrowUp = CreateIcon(5, "CircleArrowUp");
        public static readonly PriorityIcon CircleArrowDown = CreateIcon(6, "CircleArrowDown");
        public static readonly PriorityIcon CircleArrowLeft = CreateIcon(7, "CircleArrowLeft");
        public static readonly PriorityIcon CircleArrowRight = CreateIcon(8, "CircleArrowRight");
        public static readonly PriorityIcon ChevronUp = CreateIcon(9, "ChevronUp");
        public static readonly PriorityIcon ChevronDown = CreateIcon(10, "ChevronDown");
        public static readonly PriorityIcon ChevronLeft = CreateIcon(11, "ChevronLeft");
        public static readonly PriorityIcon ChevronRight = CreateIcon(12, "ChevronRight");
        public static readonly PriorityIcon AngleUp = CreateIcon(13, "AngleUp");
        public static readonly PriorityIcon AngleDown = CreateIcon(14, "AngleDown");
        public static readonly PriorityIcon AngleLeft = CreateIcon(15, "AngleLeft");
        public static readonly PriorityIcon AngleRight = CreateIcon(16, "AngleRight");
        public static readonly PriorityIcon AnglesUp = CreateIcon(17, "AnglesUp");
        public static readonly PriorityIcon AnglesDown = CreateIcon(18, "AnglesDown");
        public static readonly PriorityIcon AnglesLeft = CreateIcon(19, "AnglesLeft");
        public static readonly PriorityIcon AnglesRight = CreateIcon(20, "AnglesRight");
        public static readonly PriorityIcon Exclaimation = CreateIcon(21, "Exclaimation");
        public static readonly PriorityIcon CircleExclaimation = CreateIcon(22, "CircleExclaimation");
        public static readonly PriorityIcon TriangleExclaimation = CreateIcon(23, "TriangleExclaimation");
        public static readonly PriorityIcon XMark = CreateIcon(24, "XMark");
        public static readonly PriorityIcon Ban = CreateIcon(25, "Ban");
        public static readonly PriorityIcon EqualsSign = CreateIcon(26, "Equals");

        private static PriorityIcon CreateIcon(int id, string name)
        {
            var icon = new PriorityIcon { Id = id, Name = name };
            _icons.Add(icon);
            return icon;
        }

        public static ReadOnlyCollection<PriorityIcon> GetAll()
        {
            return _icons.AsReadOnly();
        }
    }
}
