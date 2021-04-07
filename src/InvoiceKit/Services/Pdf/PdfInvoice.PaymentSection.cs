using InvoiceKit.Helpers;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using MigraDocCore.DocumentObjectModel.Shapes;
using MigraDocCore.DocumentObjectModel.Tables;
using PdfSharpCore.Utils;
using SixLabors.ImageSharp.PixelFormats;

namespace InvoiceKit.Pdf
{
    public partial class PdfInvoice
    {
        private void PaymentSection()
        {
            Section section = Pdf.LastSection;

            Table table = section.AddTable();
            table.AddColumn(Unit.FromPoint(section.Document.PageWidth()));
            Row row = table.AddRow();

            if (Invoice.Details != null && Invoice.Details.Count > 0)
            {
                foreach (DetailRow detail in Invoice.Details)
                {
                    row.Cells[0].AddParagraph(detail.Title, ParagraphAlignment.Left, "H2-9B-Color");
                    row.Cells[0].Borders.Bottom = BorderLine;

                    row = table.AddRow();
                    TextFrame frame = null;
                    foreach (string line in detail.Paragraphs)
                    {
                        if (line == detail.Paragraphs[0])
                        {
                            frame = row.Cells[0].AddTextFrame();
                            frame.Width = section.Document.PageWidth();
                        }
                        frame.AddParagraph(line, ParagraphAlignment.Left, "H2-9");
                    }
                }
            }

            if (Invoice.Company.HasCompanyNumber || Invoice.Company.HasVatNumber)
            {
                row = table.AddRow();
                row.Height = Unit.FromCentimeter(1);
                Color shading = MigraDocHelpers.TextColorFromHtml(Invoice.TextColor);

                if (Invoice.Company.HasCompanyNumber && Invoice.Company.HasVatNumber)
                {
                    row.Cells[0]
                    .AddParagraph(string.Format("Company Number: {0}, VAT Number: {1}",
                        Invoice.Company.CompanyNumber, Invoice.Company.VatNumber),
                        ParagraphAlignment.Center, "H2-9B-Inverse")
                        .Format.Shading.Color = shading;
                }
                else
                {
                    if (Invoice.Company.HasCompanyNumber)
                        row.Cells[0].AddParagraph(string.Format("Company Number: {0}", Invoice.Company.CompanyNumber),
                        ParagraphAlignment.Center, "H2-9B-Inverse")
                        .Format.Shading.Color = shading;
                    else
                        row.Cells[0].AddParagraph(string.Format("VAT Number: {0}", Invoice.Company.VatNumber),
                        ParagraphAlignment.Center, "H2-9B-Inverse")
                        .Format.Shading.Color = shading;
                }
            }
        }


    }
}