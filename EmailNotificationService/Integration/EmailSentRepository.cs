using EmailNotificationService.Domain.Interfaces;
using EmailNotificationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailNotificationService.Integration
{
    public class EmailSentRepository : IEmailSentRepository
    {
        // This repository would be integrated to a database structure owned by this Function, for example a CosmosDB.
        // As the responsible service for e-mail sending, it would own information of how many e-mails were sent to every user.

        // This list is used only as an example
        private readonly List<EmailSent> _emailSentList;

        public EmailSentRepository()
        {
            _emailSentList = new List<EmailSent>
            {
                new EmailSent{ To = "user1@domain.com", Type = "Status", Time = DateTime.Now.AddMinutes(-5) },
                new EmailSent{ To = "user1@domain.com", Type = "Status", Time = DateTime.Now.AddMinutes(-3) },
                new EmailSent{ To = "user1@domain.com", Type = "Marketing", Time = DateTime.Now.AddMinutes(-1) },
                new EmailSent{ To = "user1@domain.com", Type = "Status", Time = DateTime.Now.AddMinutes(-1) },
                new EmailSent{ To = "user1@domain.com", Type = "Status", Time = DateTime.Now },
                new EmailSent{ To = "user1@domain.com", Type = "Status", Time = DateTime.Now },
                new EmailSent{ To = "visitant2@domain.com", Type = "News", Time = DateTime.Now.AddDays(-2) },
                new EmailSent{ To = "visitant2@domain.com", Type = "News", Time = DateTime.Now.AddHours(-6) },
                new EmailSent{ To = "visitant2@domain.com", Type = "Status", Time = DateTime.Now },
            };
        }

        public async Task<int> CountEmailSentInTimeRangeByTypeAsync(string user, string type, int searchTimeInMinutes)
        {
            DateTime timeRangeStart = DateTime.Now.AddMinutes(-searchTimeInMinutes);

            return _emailSentList.Count(email => email.To == user && email.Time >= timeRangeStart);

        }

        public async Task RegisterEmailSentAsync(string user, string type)
        {
            EmailSent newEmailSent = new EmailSent { To = user, Type = type, Time = DateTime.Now };
            _emailSentList.Add(newEmailSent);
        }
    }
}
