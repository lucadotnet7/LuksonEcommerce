namespace Ecommerce.WebApp.Model.Exceptions
{
    public class ProductException : Exception
    {
        public ProductException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
