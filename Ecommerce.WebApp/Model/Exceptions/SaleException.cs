namespace Ecommerce.WebApp.Model.Exceptions
{
    public class SaleException : Exception
    {
        public SaleException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
