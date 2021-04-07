using System.Drawing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using InvoiceKit.Exceptions;
using JetBrains.Annotations;

namespace InvoiceKit.Themes
{
    public class Theme
    {
        public string TextColor { get; set; }
        public string BackColor { get; set; }


        public Theme(
            [NotNull] string textColor,
            [NotNull] string backColor)
        {

            TextColor = ValidateColor(textColor);
            BackColor = ValidateColor(backColor);
        }
        public static Theme CreateTheme(
            ThemeOptions theme,
            string textColor = null,
            string backColor = null)
        {

            var colorsDict = new Dictionary<ThemeOptions, Theme>();

            colorsDict.Add(ThemeOptions.ORANGE_GROOVE, new Theme("#fb8500", "#ffbf69"));
            colorsDict.Add(ThemeOptions.BLUE_AS_THE_OCEAN, new Theme("#219ebc", "#8ecae6"));
            colorsDict.Add(ThemeOptions.RED_DOESNT_MEAN_DANGER, new Theme("#9a031e", "#FFD6CC"));
            colorsDict.Add(ThemeOptions.GREEN_LIKE_THE_PLANTS, new Theme("#64a824", "#aad26e"));

            if (textColor is not null && backColor is not null)
                return new Theme(ValidateColor(textColor), ValidateColor(backColor));

            return colorsDict[theme];
        }

        private static string ValidateColor(string color)
        {
            var isHex = false;
            if (color.StartsWith("#"))
            {
                color = color.Remove(0, 1);
            }

            if (color.Length > 0 && color.Length <= 32)
            {
                var hexcolor = color.PadLeft(32, '0');
                Guid guid;
                isHex = Guid.TryParse(hexcolor, out guid);
            }
            else
            {
                //some other way to test if 
                Regex myRegex = new Regex("^[a-fA-F0-9]+$");
                isHex = myRegex.IsMatch(color);
            }
            if (isHex == false) throw new InvalidHexColorException(color);

           color = color.Insert(0, "#").ToString();
            return color;
        }
    }
}