namespace EmailNotificationService.Domain.Exceptions
{
    public class MissingEmailInformationException : Exception
    {
        public MissingEmailInformationException() { }

        public MissingEmailInformationException(string message)
            : base(message) { }

        public MissingEmailInformationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
