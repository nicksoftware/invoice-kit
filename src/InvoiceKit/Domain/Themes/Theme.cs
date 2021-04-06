using System.Collections.Generic;

namespace InvoiceKit.Themes
{
    public static class ThemeOptions
    {

        public const string OrangeGroove = "Orange Groove";
        public const string BlueAsTheOcean = "Blue as the Ocean";
        public const string RedDoesntMeanDanger = "Red doesn't mean Danger";
        public const string GreenForThePlants = "Green just like the grass";
    }
    public class Theme
    {
        public string TextColor { get; set; }
        public string BackColor { get; set; }


        public Theme(string textColor, string backColor)
        {
            TextColor = textColor;
            BackColor = backColor;
        }
        public static Theme UseTheme(string theme)
        {
            var colorsDict = new Dictionary<string, Theme>();

            colorsDict.Add(ThemeOptions.OrangeGroove, new Theme("#fb8500", "#ffbf69"));
            colorsDict.Add(ThemeOptions.BlueAsTheOcean, new Theme("#219ebc", "#8ecae6"));
            return colorsDict[theme];
        }
    }
}