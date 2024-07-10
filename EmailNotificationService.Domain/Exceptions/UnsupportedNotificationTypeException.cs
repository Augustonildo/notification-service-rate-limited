namespace EmailNotificationService.Domain.Exceptions
{
    public class UnsupportedNotificationTypeException : Exception
    {
        public UnsupportedNotificationTypeException() { }

        public UnsupportedNotificationTypeException(string message)
            : base(message) { }

        public UnsupportedNotificationTypeException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
