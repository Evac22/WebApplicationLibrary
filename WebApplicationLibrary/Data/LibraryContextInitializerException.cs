namespace WebApplicationLibrary.Data
{
    public class LibraryContextInitializerException : Exception
    {
        public LibraryContextInitializerException()
        {
        }

        public LibraryContextInitializerException(string message)
            : base(message)
        {
        }

        public LibraryContextInitializerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}