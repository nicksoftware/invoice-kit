using System.Collections.Generic;
using InvoiceKit.Themes;
using InvoiceKit;
using Spectre.Console;

namespace InvoiceKit.Cli
{

    public class InvoiceCreator
    {
        public string TextColor { get; set; }
        public string BackColor { get; set; }
        public string InnvoiceColor { get; set; }
        public string CurrencySymbol { get; set; }
        public SizeOption PageSize { get; set; }
        public OrientationOption PageOrientation { get; set; }
        public BrandImage BrandImage { get; set; }

        public void Start()
        {
            //Get COmpany name
            //Company Address
            Create();
        }

        public void Create()
        {
            var theme = Theme.UseTheme(ThemeOptions.BlueAsTheOcean);
            new InvoiceBuilder(SizeOption.A4, OrientationOption.Landscape, "R")
                .TextColor(theme.TextColor)
                .BackColor(theme.BackColor)
                .Image(@"invoice.png", 125, 27)
                .Company(Address.Make("FROM", new string[] { "Devmania", "Cautious Inventions", "40 Portland Avenue", "Hurst Hill", "Johannesburg" }, "2092"))
                .Client(Address.Make("BILLING TO", new string[] { "Khozi Properties", "Student Accommodations", "Melville", "Johannesburg", "2094" }))
                .Items(new List<ItemRow> {
                    ItemRow.Make("Stuff Manangement System", "React Frontend , Localization included,Role Management,Notification System", (decimal)45_0000, 20, (decimal)166.66, (decimal)45_1230),
                    ItemRow.Make("Android App", "Allow users to check in,Role management, Task Management", (decimal)34_000, 20, (decimal)360.00, (decimal)34_432.00),
                    ItemRow.Make("iOS App", "same as android app", (decimal)34_000, 0, (decimal)0, (decimal)0),
                    ItemRow.Make("", "", (decimal)0, 0, (decimal)0, (decimal)0),
                })
                .Totals(new List<TotalRow> {
                    TotalRow.Make("Sub Total", (decimal)526.66),
                    TotalRow.Make("VAT @ 20%", (decimal)105.33),
                    TotalRow.Make("Total", (decimal)631.99, true),
                })
                .Details(new List<DetailRow> {
                    DetailRow.Make("PAYMENT INFORMATION",
                    "Make all cheques payable to Cautious Inventions.",
                    "",
                    "If you have any questions concerning this invoice, contact our sales department at sales@CautiousInventions.co.za.",
                    "",
                    "Thank you for your business.")
                })
                .Footer("http://www.CautiousInventions.co.za")
                .Save();
        }
    }
}