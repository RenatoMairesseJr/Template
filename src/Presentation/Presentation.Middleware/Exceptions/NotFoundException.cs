namespace Presentation.Middleware.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"{name} {key}")
        {

        }
    }
}
