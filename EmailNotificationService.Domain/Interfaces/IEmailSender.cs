namespace EmailNotificationService.Domain.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(string email, string message);
    }
}
