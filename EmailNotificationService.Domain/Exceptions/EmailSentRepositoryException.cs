namespace EmailNotificationService.Domain.Exceptions
{
    public class EmailSentRepositoryException : Exception
    {
        public EmailSentRepositoryException() { }

        public EmailSentRepositoryException(string message)
            : base(message) { }

        public EmailSentRepositoryException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
