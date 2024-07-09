namespace EmailNotificationService.Domain.Interfaces
{
    public interface IEmailSender
    {
        void Send(string email, string message);
    }
}
