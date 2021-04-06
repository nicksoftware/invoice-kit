
namespace InvoiceKit.Helpers
{
    public static class Utilites
    {
        public static string ToCurrency(this decimal number)
        {
            return ToCurrency(number, string.Empty);
        }

        public static string ToCurrency(this decimal number, string symbol)
        {
            return string.Format("{0}{1:N2}", symbol, number);
        }

        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T tmp = lhs; lhs = rhs; rhs = tmp;
        }
    }
}
