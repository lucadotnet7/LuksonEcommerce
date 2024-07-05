namespace Ecommerce.WebApp.Model.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
