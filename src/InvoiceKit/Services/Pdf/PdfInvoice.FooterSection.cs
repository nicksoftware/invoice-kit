using InvoiceKit.Helpers;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Tables;


namespace InvoiceKit.Pdf
{
    public partial class PdfInvoice
    {
        public void FooterSection()
        {
            HeaderFooter footer = Pdf.LastSection.Footers.Primary;

            Table table = footer.AddTable();
            table.AddColumn(footer.Section.PageWidth() / 2);
            table.AddColumn(footer.Section.PageWidth() / 2);
            Row row = table.AddRow();
            if (!string.IsNullOrEmpty(Invoice.Footer))
            {
                Paragraph paragraph = row.Cells[0].AddParagraph(Invoice.Footer, ParagraphAlignment.Left, "H2-8-Blue");
                _ = paragraph.AddHyperlink(Invoice.Footer, HyperlinkType.Web);
            }

            Paragraph info = row.Cells[1].AddParagraph();
            info.Format.Alignment = ParagraphAlignment.Right;
            info.Style = "H2-8";
            info.AddText("Page ");
            info.AddPageField();
            info.AddText(" of ");
            info.AddNumPagesField();
        }


    }
}