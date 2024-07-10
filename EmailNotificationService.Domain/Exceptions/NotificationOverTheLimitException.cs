namespace EmailNotificationService.Domain.Exceptions
{
    public class NotificationOverTheLimitException : Exception
    {
        public NotificationOverTheLimitException() { }

        public NotificationOverTheLimitException(string message)
            : base(message) { }

        public NotificationOverTheLimitException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
