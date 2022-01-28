using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Domain.Entities;

namespace WhatBug.Domain.Data
{
    public class Colors
    {
        private static readonly List<Color> _colors = new List<Color>();

        // Whites
        public static readonly Color White = CreateColor(1, "White");
        public static readonly Color Chiffon = CreateColor(2, "Chiffon");
        public static readonly Color Frost = CreateColor(3, "Frost");

        // Tans
        public static readonly Color Tan = CreateColor(4, "Tan");
        public static readonly Color Buttermilk = CreateColor(5, "Buttermilk");
        public static readonly Color Beige = CreateColor(6, "Beige");
        public static readonly Color Oat = CreateColor(7, "Oat");
        public static readonly Color Cookie = CreateColor(8, "Cookie");
        public static readonly Color Latte = CreateColor(9, "Latte");

        // Yellows
        public static readonly Color Lemon = CreateColor(10, "Lemon");
        public static readonly Color Yellow = CreateColor(11, "Yellow");
        public static readonly Color Honey = CreateColor(12, "Honey");

        // Oranges
        public static readonly Color Gold = CreateColor(13, "Gold");
        public static readonly Color Apricot = CreateColor(14, "Apricot");
        public static readonly Color Orange = CreateColor(15, "Orange");

        // Reds
        public static readonly Color Blush = CreateColor(16, "Blush");
        public static readonly Color Red = CreateColor(17, "Red");
        public static readonly Color Rose = CreateColor(18, "Rose");
        public static readonly Color Candy = CreateColor(19, "Candy");
        public static readonly Color Apple = CreateColor(20, "Apple");
        public static readonly Color Cherry = CreateColor(21, "Cherry");

        // Pinks
        public static readonly Color Pink = CreateColor(22, "Pink");
        public static readonly Color Fushcia = CreateColor(23, "Fushcia");
        public static readonly Color Hotpink = CreateColor(24, "Hotpink");
        public static readonly Color Magenta = CreateColor(25, "Magenta");

        // Purples
        public static readonly Color Lilac = CreateColor(26, "Lilac");
        public static readonly Color Iris = CreateColor(27, "Iris");
        public static readonly Color Amethyst = CreateColor(28, "Amethyst");
        public static readonly Color Purple = CreateColor(29, "Purple");
        public static readonly Color Violet = CreateColor(30, "Violet");

        // Blues
        public static readonly Color Sky = CreateColor(31, "Sky");
        public static readonly Color Sapphire = CreateColor(32, "Sapphire");
        public static readonly Color Cerulean = CreateColor(33, "Cerulean");
        public static readonly Color Blue = CreateColor(34, "Blue");
        public static readonly Color Cobalt = CreateColor(35, "Cobalt");
        public static readonly Color Navy = CreateColor(36, "Navy");

        // Greens
        public static readonly Color Mint = CreateColor(37, "Mint");
        public static readonly Color Seafoam = CreateColor(38, "Seafoam");
        public static readonly Color Lime = CreateColor(39, "Lime");
        public static readonly Color Olive = CreateColor(40, "Olive");
        public static readonly Color Pear = CreateColor(41, "Pear");
        public static readonly Color Pickle = CreateColor(42, "Pickle");
        public static readonly Color Green = CreateColor(43, "Green");
        public static readonly Color Shamrock = CreateColor(44, "Shamrock");
        public static readonly Color Emerald = CreateColor(45, "Emerald");
        public static readonly Color Basil = CreateColor(46, "Basil");

        // Browns
        public static readonly Color Tortilla = CreateColor(47, "Tortilla");
        public static readonly Color Peanut = CreateColor(48, "Peanut");
        public static readonly Color Tawny = CreateColor(49, "Tawny");
        public static readonly Color Caramel = CreateColor(50, "Caramel");
        public static readonly Color Coffee = CreateColor(51, "Coffee");
        public static readonly Color Mocha = CreateColor(52, "Mocha");
        public static readonly Color Hickory = CreateColor(53, "Hickory");
        public static readonly Color Brown = CreateColor(54, "Brown");

        // Grays
        public static readonly Color Cloud = CreateColor(55, "Cloud");
        public static readonly Color Silver = CreateColor(56, "Silver");
        public static readonly Color Coin = CreateColor(57, "Coin");
        public static readonly Color Ash = CreateColor(58, "Ash");
        public static readonly Color Gray = CreateColor(59, "Gray");
        public static readonly Color Pewter = CreateColor(60, "Pewter");
        public static readonly Color Smoke = CreateColor(61, "Smoke");
        public static readonly Color Slate = CreateColor(62, "Slate");

        // Blacks
        public static readonly Color Black = CreateColor(63, "Black");

        private static Color CreateColor(int id, string name)
        {
            var color = new Color() { Id = id, Name = name };
            _colors.Add(color);
            return color;
        }

        public static ReadOnlyCollection<Color> Seed()
        {
            return _colors.AsReadOnly();
        }
    }
}
