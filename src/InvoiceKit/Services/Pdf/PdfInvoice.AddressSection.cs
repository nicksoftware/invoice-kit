using InvoiceKit.Helpers;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Tables;
namespace InvoiceKit.Pdf
{
    public partial class PdfInvoice
    {
        private void AddressSection()
        {
            Section section = Pdf.LastSection;

            Address leftAddress = Invoice.Company;
            Address rightAddress = Invoice.Client;

            if (Invoice.CompanyOrientation == PositionOption.Right)
                Utilites.Swap<Address>(ref leftAddress, ref rightAddress);

            Table table = section.AddTable();
            table.AddColumn(ParagraphAlignment.Left, section.Document.PageWidth() / 2 - 10);
            table.AddColumn(ParagraphAlignment.Center, Unit.FromPoint(20));
            table.AddColumn(ParagraphAlignment.Left, section.Document.PageWidth() / 2 - 10);

            Row row = table.AddRow();
            row.Style = "H2-10B-Color";
            row.Shading.Color = Colors.White;

            row.Cells[0].AddParagraph(leftAddress.Title, ParagraphAlignment.Left);
            row.Cells[0].Format.Borders.Bottom = BorderLine;
            row.Cells[2].AddParagraph(rightAddress.Title, ParagraphAlignment.Left);
            row.Cells[2].Format.Borders.Bottom = BorderLine;

            row = table.AddRow();
            AddressCell(row.Cells[0], leftAddress.AddressLines);
            AddressCell(row.Cells[2], rightAddress.AddressLines);

            _ = table.AddRow();
        }

        private void AddressCell(Cell cell, string[] address)
        {
            foreach (string line in address)
            {
                Paragraph name = cell.AddParagraph();
                if (line == address[0])
                    name.AddFormattedText(line, "H2-10B");
                else
                    name.AddFormattedText(line, "H2-9-Grey");
            }
        }

    }
}