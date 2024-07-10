using EmailNotificationService.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace EmailNotificationService.Integration
{
    public class EmailSentRepository : IEmailSentRepository
    {
        public EmailSentRepository() { }

        public async Task<int> CountEmailSentInTimeRangeByTypeAsync(string user, string type, int searchTimeInMinutes)
        {
            // TODO: Implement local solution
            throw new NotImplementedException();
        }

        public async Task RegisterEmailSentAsync(string user, string type)
        {
            // TODO: describe solution
            throw new NotImplementedException();
        }
    }
}
