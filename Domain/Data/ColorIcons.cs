using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Entities;

namespace WhatBug.Domain.Data
{
    public class ColorIcons
    {
        private static readonly List<ColorIcon> _colorIcons = new List<ColorIcon>();

        public static readonly ColorIcon BlueTick = CreateColorIcon(1, Icons.CheckSquare, Colors.Blue);
        public static readonly ColorIcon RedBug = CreateColorIcon(2, Icons.Bug, Colors.Candy);

        private static ColorIcon CreateColorIcon(int id, Icon icon, Color color)
        {
            var colorIcon = new ColorIcon { Id = id, Icon = icon, Color = color };
            _colorIcons.Add(colorIcon);
            return colorIcon;
        }

        public static ReadOnlyCollection<ColorIcon> Seed()
        {
            return _colorIcons.Select(i => new ColorIcon { Id = i.Id, IconId = i.Icon.Id, ColorId = i.Color.Id }).ToList().AsReadOnly();
        }
    }
}
