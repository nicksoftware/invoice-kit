using System.Collections.Generic;

namespace InvoiceKit.Themes
{
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
            colorsDict.Add(ThemeOptions.RedDoesntMeanDanger, new Theme("#9a031e", "#FFD6CC"));
            colorsDict.Add(ThemeOptions.GreenForThePlants, new Theme("#64a824", "#aad26e"));
            return colorsDict[theme];
        }
    }
}