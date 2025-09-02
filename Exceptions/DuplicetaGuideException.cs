namespace server.Exceptions
{
    /// <summary>
    /// Классы, описывающий ошибку, при попытке добавления в БД записи с уже существующим первичным ключем
    /// </summary>
    public class DuplicateGuideException : Exception
    {
        public DuplicateGuideException() { }

        public DuplicateGuideException(string message)
            : base(message) { }

        public DuplicateGuideException(string message, Exception innerException)
            : base(message, innerException) { }
    }

}
