
namespace GameScheduler.BLL.Exceptions
{
    public class RequestValidationException : ApplicationSystemBaseException
    {
        public RequestValidationException(string message) : base(message)
        {
        }
    }
}
