using System;
namespace InvoiceKit.Exceptions
{
    public class InvalidHexColorException : BusinessException
    {
        public InvalidHexColorException()
        {
        }

        public InvalidHexColorException(string color)
        : base($"Invalid hex, Color Invoice Kit cannot process hex Color {color}")
        {
        }
        public InvalidHexColorException(string color, Exception innerException)
            : base(string.Format("Invalid hex Color Invoice Kit cannot process hex Color {0}", color), innerException)
        {
        }
    }
}