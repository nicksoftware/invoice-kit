namespace InvoiceKit
{
    public class BrandImage
    {
        public BrandImage(string imageUrl)
        {
        }
        public string ImageUrl { get; set; }
        public int Width { get; set; } = 125;
        public int Height { get; set; } = 27;
    }
}