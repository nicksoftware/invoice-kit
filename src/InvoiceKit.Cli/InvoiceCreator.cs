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

            //string name = AnsiConsole.Ask<string>("What's your [green]name[/]?");
            Create();
        }

        public void Create()
        {
            var theme = Theme.UseTheme(ThemeOptions.OrangeGroove);
            new InvoiceBuilder(SizeOption.A4, OrientationOption.Landscape, "£")
                .TextColor(theme.TextColor)
                .BackColor(theme.BackColor)
                .Image(@"invoice.png", 125, 27)
                .Company(Address.Make("FROM", new string[] { "Vodafone Limited", "Vodafone House", "The Connection", "Newbury", "Berkshire RG14 2FN" }, "1471587", "569953277"))
                .Client(Address.Make("BILLING TO", new string[] { "Isabella Marsh", "Overton Circle", "Little Welland", "Worcester", "WR## 2DJ" }))
                .Items(new List<ItemRow> {
                    ItemRow.Make("Nexus 6", "Midnight Blue", (decimal)1, 20, (decimal)166.66, (decimal)199.99),
                    ItemRow.Make("24 Months (£22.50pm)", "100 minutes, Unlimited texts, 100 MB data 3G plan with 3GB of UK Wi-Fi", (decimal)1, 20, (decimal)360.00, (decimal)432.00),
                    ItemRow.Make("Special Offer", "Free case (blue)", (decimal)1, 0, (decimal)0, (decimal)0),
                })
                .Totals(new List<TotalRow> {
                    TotalRow.Make("Sub Total", (decimal)526.66),
                    TotalRow.Make("VAT @ 20%", (decimal)105.33),
                    TotalRow.Make("Total", (decimal)631.99, true),
                })
                .Details(new List<DetailRow> {
                    DetailRow.Make("PAYMENT INFORMATION",
                    "Make all cheques payable to Vodafone UK Limited.",
                    "",
                    "If you have any questions concerning this invoice, contact our sales department at sales@vodafone.co.uk.",
                    "",
                    "Thank you for your business.")
                })
                .Footer("http://www.vodafone.co.uk")
                .Save();
        }
    }
}