namespace EmailNotificationService.Domain.Interfaces
{
    public interface IEmailSentRepository
    {
        Task<int> CountEmailSentInTimeRangeByTypeAsync(string user, string type, int searchTimeInMinutes);
        Task RegisterEmailSentAsync(string user, string type);
    }
}
