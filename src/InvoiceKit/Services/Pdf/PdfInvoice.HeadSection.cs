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
        private void HeaderSection()
        {
            HeaderFooter header = Pdf.LastSection.Headers.Primary;

            Table table = header.AddTable();
            double thirdWidth = Pdf.PageWidth() / 3;

            table.AddColumn(ParagraphAlignment.Left, thirdWidth * 2);
            table.AddColumn();

            Row row = table.AddRow();

            if (!string.IsNullOrEmpty(Invoice.Image))
            {
                if (ImageSource.ImageSourceImpl == null)
                    ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();

                Image image = row.Cells[0].AddImage(ImageSource.FromFile(Invoice.Image));
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

                image.Height = Invoice.ImageSize.Height;
                image.Width = Invoice.ImageSize.Width;
            }

            TextFrame frame = row.Cells[1].AddTextFrame();

            Table subTable = frame.AddTable();
            subTable.AddColumn(thirdWidth / 2);
            subTable.AddColumn(thirdWidth / 2);

            row = subTable.AddRow();
            row.Cells[0].MergeRight = 1;
            row.Cells[0].AddParagraph(Invoice.Title, ParagraphAlignment.Right, "H1-20");

            row = subTable.AddRow();
            row.Cells[0].AddParagraph("REFERENCE:", ParagraphAlignment.Left, "H2-9B-Color");
            row.Cells[1].AddParagraph(Invoice.Reference, ParagraphAlignment.Right, "H2-9");
            row.Cells[0].AddParagraph("BILLING DATE:", ParagraphAlignment.Left, "H2-9B-Color");
            row.Cells[1].AddParagraph(Invoice.BillingDate.ToShortDateString(), ParagraphAlignment.Right, "H2-9");
            row.Cells[0].AddParagraph("DUE DATE:", ParagraphAlignment.Left, "H2-9B-Color");
            row.Cells[1].AddParagraph(Invoice.DueDate.ToShortDateString(), ParagraphAlignment.Right, "H2-9");
        }


    }
}