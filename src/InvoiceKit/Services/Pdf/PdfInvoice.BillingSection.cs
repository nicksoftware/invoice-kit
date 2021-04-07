
using InvoiceKit.Helpers;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Tables;
namespace InvoiceKit.Pdf
{
    public partial class PdfInvoice
    {
        private void BillingSection()
        {
            Section section = Pdf.LastSection;

            Table table = section.AddTable();

            double width = section.PageWidth();
            double productWidth = Unit.FromPoint(150);
            double numericWidth = (width - productWidth) / (Invoice.HasDiscount ? 5 : 4);
            table.AddColumn(productWidth);
            table.AddColumn(ParagraphAlignment.Center, numericWidth);
            table.AddColumn(ParagraphAlignment.Center, numericWidth);
            table.AddColumn(ParagraphAlignment.Center, numericWidth);
            if (Invoice.HasDiscount)
                table.AddColumn(ParagraphAlignment.Center, numericWidth);
            table.AddColumn(ParagraphAlignment.Center, numericWidth);

            BillingHeader(table);

            foreach (ItemRow item in Invoice.Items)
            {
                BillingRow(table, item);
            }

            if (Invoice.Totals != null)
            {
                foreach (TotalRow total in Invoice.Totals)
                {
                    BillingTotal(table, total);
                }
            }
            table.AddRow();
        }

        private void BillingHeader(Table table)
        {
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Height = Unit.FromCentimeter(1.0);
            row.Style = "H2-10B-Color";
            row.Shading.Color = Colors.WhiteSmoke;
            row.TopPadding = 10;
            row.Borders.Bottom = BorderLine;

            row.Cells[0].AddParagraph("PRODUCT", ParagraphAlignment.Left);
            row.Cells[1].AddParagraph("AMOUNT", ParagraphAlignment.Center);
            // row.Cells[2].AddParagraph("VAT %", ParagraphAlignment.Center);
            row.Cells[3].AddParagraph("UNIT PRICE", ParagraphAlignment.Center);
            if (Invoice.HasDiscount)
            {
                row.Cells[4].AddParagraph("DISCOUNT", ParagraphAlignment.Center);
                row.Cells[5].AddParagraph("TOTAL", ParagraphAlignment.Center);
            }
            else
            {
                row.Cells[4].AddParagraph("TOTAL", ParagraphAlignment.Center);
            }
        }

        private void BillingRow(Table table, ItemRow item)
        {
            Row row = table.AddRow();
            row.Style = "TableRow";
            row.Shading.Color = MigraDocHelpers.BackColorFromHtml(Invoice.BackColor);

            Cell cell = row.Cells[0];
            cell.AddParagraph(item.Name, ParagraphAlignment.Left, "H2-9B");
            cell.AddParagraph(item.Description, ParagraphAlignment.Left, "H2-9-Grey");

            cell = row.Cells[1];
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(item.Amount.ToCurrency(), ParagraphAlignment.Center, "H2-9");

            // cell = row.Cells[2];
            // cell.VerticalAlignment = VerticalAlignment.Center;
            // cell.AddParagraph(item.VAT.ToCurrency(), ParagraphAlignment.Center, "H2-9");

            cell = row.Cells[3];
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(item.Price.ToCurrency(Invoice.Currency), ParagraphAlignment.Center, "H2-9");

            if (Invoice.HasDiscount)
            {
                cell = row.Cells[4];
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.AddParagraph(item.Discount, ParagraphAlignment.Center, "H2-9");

                cell = row.Cells[5];
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.AddParagraph(item.Total.ToCurrency(Invoice.Currency), ParagraphAlignment.Center, "H2-9");
            }
            else
            {
                cell = row.Cells[4];
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.AddParagraph(item.Total.ToCurrency(Invoice.Currency), ParagraphAlignment.Center, "H2-9");
            }
        }

        private void BillingTotal(Table table, TotalRow total)
        {
            if (Invoice.HasDiscount)
            {
                table.Columns[4].Format.Alignment = ParagraphAlignment.Left;
                table.Columns[5].Format.Alignment = ParagraphAlignment.Left;
            }
            else
            {
                table.Columns[4].Format.Alignment = ParagraphAlignment.Left;
            }

            Row row = table.AddRow();
            row.Style = "TableRow";

            string font; Color shading;
            if (total.Inverse == true)
            {
                font = "H2-9B-Inverse";
                shading = MigraDocHelpers.TextColorFromHtml(Invoice.TextColor);
            }
            else
            {
                font = "H2-9B";
                shading = MigraDocHelpers.BackColorFromHtml(Invoice.BackColor);
            }

            if (Invoice.HasDiscount)
            {
                Cell cell = row.Cells[4];
                cell.Shading.Color = shading;
                cell.AddParagraph(total.Name, ParagraphAlignment.Left, font);

                cell = row.Cells[5];
                cell.Shading.Color = shading;
                cell.AddParagraph(total.Value.ToCurrency(Invoice.Currency), ParagraphAlignment.Center, font);
            }
            else
            {
                Cell cell = row.Cells[3];
                cell.Shading.Color = shading;
                cell.AddParagraph(total.Name, ParagraphAlignment.Left, font);

                cell = row.Cells[4];
                cell.Shading.Color = shading;
                cell.AddParagraph(total.Value.ToCurrency(Invoice.Currency), ParagraphAlignment.Center, font);
            }
        }


    }
}