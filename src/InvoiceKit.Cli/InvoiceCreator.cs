using System;
using System.Collections.Generic;
using InvoiceKit.Themes;
using InvoiceKit;
using Spectre.Console;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading;

namespace InvoiceKit.Cli
{

    public class InvoiceCreator
    {
        private string CurrencySymbol = "R";
        private SizeOption PageSize { get; set; } = SizeOption.A4;
        private OrientationOption PageOrientation { get; set; } = OrientationOption.Landscape;
        private BrandImage BrandImage { get; set; } = new BrandImage("invoice.png");
        private ThemeOptions ThemeSelected { get; set; } = ThemeOptions.BLUE_AS_THE_OCEAN;
        private string companyAddress = string.Empty;
        public string Email { get; set; }
        public string WebSite { get; set; }
        private string clientAddress = string.Empty;
        private string ClientName { get; set; }
        private string RegistrationNumber { get; set; }
        private readonly Dictionary<string, ThemeOptions> _themeCollection;
        private string _pdfOutputPath = string.Empty;
        private string _fileName = "invoice_" + DateTime.Now.ToString("dd_MM_yyyy");
        private string CompanyName { get; set; }
        private List<ItemRow> _items = new List<ItemRow>();

        public InvoiceCreator()
        {
            _themeCollection = new Dictionary<string, ThemeOptions>();
            _themeCollection.Add(nameof(ThemeOptions.BLUE_AS_THE_OCEAN), ThemeOptions.BLUE_AS_THE_OCEAN);
            _themeCollection.Add(nameof(ThemeOptions.GREEN_LIKE_THE_PLANTS), ThemeOptions.GREEN_LIKE_THE_PLANTS);
            _themeCollection.Add(nameof(ThemeOptions.ORANGE_GROOVE), ThemeOptions.ORANGE_GROOVE);
            _themeCollection.Add(nameof(ThemeOptions.RED_DOESNT_MEAN_DANGER), ThemeOptions.RED_DOESNT_MEAN_DANGER);
        }
        public void Start()
        {
            ClearScreen();

            CompanyName = AnsiConsole.Ask<string>("What is your [green]Company[/] Called ?");
            Email = AnsiConsole.Ask<string>("Please enter your [bold]Company's[/] Email address?");
            WebSite = AnsiConsole.Ask<string>("Please enter your [bold]Company's[/] Website Url");
            RegistrationNumber = AnsiConsole.Ask<string>("Enter your Company's VAT number");
            companyAddress = PromptAddress("Company's");
            ClearScreen();

            var themeName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Which Theme would you like to use on your Invoice?")
                .AddChoices<string>(new string[]{
                    $"{nameof(ThemeOptions.BLUE_AS_THE_OCEAN)}",
                    $"{nameof(ThemeOptions.GREEN_LIKE_THE_PLANTS)}",
                    $"{nameof(ThemeOptions.ORANGE_GROOVE)}",
                    $"{nameof(ThemeOptions.RED_DOESNT_MEAN_DANGER)}"
                })
            );
            ClearScreen();
            CurrencySymbol = AnsiConsole.Ask<string>("Enter the Currency Symbol you want to use. (e.g $)");
            ClearScreen();

            ClientName = AnsiConsole.Ask<string>("Please enter your Client's full names");

            clientAddress = PromptAddress("Client's");
            ClearScreen();

            do
            {
                AnsiConsole.MarkupLine("[yellow] Invoice Items and their costs[/]");
                Console.WriteLine();
                PromptInvoiceItem();

                if (!AnsiConsole.Confirm("Do you want to add another Item ?"))
                {
                    break;
                }
            } while (true);

            PromprFileOutput();
            _fileName = AnsiConsole.Ask<string>("Enter invoice output file name");
            ClearScreen();
            ThemeSelected = _themeCollection[themeName];
            AnsiConsole.Status()
                .Start("Starting Render Process...", ctx =>
                {
                    // Simulate some work
                    AnsiConsole.MarkupLine("Validating Output path...");
                    Thread.Sleep(1000);
                    if (Directory.Exists(_pdfOutputPath))
                    {
                        ctx.Status("Directory Exists..");

                    }

                    // Update the status and spinner
                    ctx.Status("Validating File name and triming file name.. ");
                    if (_fileName.Contains(' '))
                    {
                        _fileName = _fileName.Replace(' ', '_');
                    }

                    ctx.Spinner(Spinner.Known.Star);

                    ctx.SpinnerStyle(Style.Parse("green"));

                    // Simulate some work
                    AnsiConsole.MarkupLine("Generating Pdf...");
                    Thread.Sleep(2000);
                });

            Create();
            var path = Directory.EnumerateDirectories(Path.Combine(_pdfOutputPath)).ToList();
            AnsiConsole.MarkupLine($"Invoice Pdf successfully Generated and stored in {Path.Combine(_pdfOutputPath, _fileName) + ".pdf"}");
        }

        private static void ClearScreen()
        {
            Console.Clear();
            Program.DisplayHeading();
        }

        private void PromptInvoiceItem()
        {
            var itemName = AnsiConsole.Ask<string>("Enter Item name");
            var itemDescription = AnsiConsole.Ask<string>($"Enter Item [green]{itemName}'s[/] Description");
            var itemCost = AnsiConsole.Ask<decimal>($"Enter item {itemName}'s Cost");
            // var itemVat = AnsiConsole.Ask<decimal>($"Enter item {itemName}'s Vat,0 is also accepted");
            var hasDiscount = AnsiConsole.Confirm($"Do you have a discount for item [green]{itemName}[/] ?");
            var discount = string.Empty;

            if (hasDiscount)
            {
                discount = AnsiConsole.Ask<string>($"Enter item [green]{itemName}'s[/] discount. e.g( 20%)");
            }

            var itemUnitPrice = AnsiConsole.Ask<decimal>($"Enter item {itemName}'s UnitPrice");
            var ItemTotalCost = AnsiConsole.Ask<decimal>($"Enter item {itemName}'s Total Cost");


            _items.Add(
                ItemRow.Make(itemName, itemDescription, itemCost, 15, itemUnitPrice, discount, ItemTotalCost));
            var root = new Tree("Invoice");
            // Add some nodes
            var table = new Table();

            table.Title = new TableTitle("Invoice Table");
            table.RoundedBorder()
                .AddColumn("Name")
                .AddColumn("Description")
                .AddColumn("Cost")
                .AddColumn("Vat")
                .AddColumn("Unit Price")
                .AddColumn("Has Discount")
                .AddColumn("Discount")
                .AddColumn("Total Cost");

            foreach (var item in _items)
            {
                table.AddRow(new[] {
                    item.Name,
                    item.Description,
                    item.Amount.ToString(),
                    "15%",
                    CurrencySymbol +item.Price.ToString(),
                    item.HasDiscount.ToString(),
                    item.Discount,
                    CurrencySymbol + item.Total.ToString()
                });
            }
            var invoiceItemsItable = root.AddNode("[yellow]Invoice Items Preview [/]");
            invoiceItemsItable.AddNode(table);
            AnsiConsole.Render(root);
        }

        private void PromprFileOutput()
        {
            _pdfOutputPath = AnsiConsole.Ask<string>("PLease enter the output path of the invoice pdf");
            Directory.CreateDirectory(_pdfOutputPath);
        }

        private string PromptAddress(string title)
        {
            var address = AnsiConsole.Prompt<string>(
                new TextPrompt<string>($"Enter your {title} address seperated by \",\" e.g (12 Cross street ,Johannesubrg,...)")
                .Validate(address =>
                {
                    return (address.Contains(",") && address.Length > 0);
                })
            );

            return address;
        }
        public void Create()
        {

            var subTotal = (decimal)_items.Sum(it => it.Total);
            var vatCost = (decimal)_items.Sum(it => it.Total) * (15 / 100);
            var total = subTotal + vatCost;

            var fromAddress = $"{CompanyName },{companyAddress}";
            var toAddress = $"{ClientName },{clientAddress}";
            new InvoiceBuilder(PageSize, PageOrientation, CurrencySymbol)
             .WithTheme(Theme.CreateTheme(ThemeSelected))
                 .Image(BrandImage)
                 .Company(Address
                    .Make(title: "FROM", address: fromAddress.Split(','), vat: RegistrationNumber))
                 .Client(Address
                    .Make("BILLING TO", toAddress.Split(',')))
                 .Items(_items)
                 .Totals(new List<TotalRow> {
                    TotalRow.Make("Sub Total", subTotal),
                    TotalRow.Make("VAT @ 15%", vatCost),
                    TotalRow.Make("Total",total),
                 })
                 .Details(new List<DetailRow> {
                    DetailRow.Make("PAYMENT INFORMATION",
                    "Make all cheques payable to Cautious Inventions.",
                    "",
                    $"If you have any questions concerning this invoice, contact our sales department at {Email}",
                    "",
                    "Thank you for your business.")
                 })
                 .Footer(WebSite)
                 .Save(Path.Combine(_pdfOutputPath, _fileName) + ".pdf");
        }
    }
}