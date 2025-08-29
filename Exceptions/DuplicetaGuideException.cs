namespace server.Exceptions
{
    public class DuplicateGuideException : Exception
    {
        public DuplicateGuideException() { }

        public DuplicateGuideException(string message)
            : base(message) { }

        public DuplicateGuideException(string message, Exception innerException)
            : base(message, innerException) { }
    }

}
