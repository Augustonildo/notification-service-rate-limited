# Notification-service-rate-limited
.NET Azure Function application that expects events from a Service Bus Queue and sends e-mails according to rate rules.

## Important Information
- Any application can feed the queue, provided that the message format is correct.
- Messages are retried X times before going to Dead Letter Queue (DLQ)
- Session is enabled: Events related to an user need to finish before another event to the same user start. 
- Notification types and respective rate limits are received from an AppConfiguration and can be changed without need to update the application's code.
- Sent e-mail information is stored in a database exclusive for this service. 

# Possible Structure
![Diagram](https://github.com/Augustonildo/notification-service-rate-limited/assets/23284555/d1101365-64d7-4339-ab1c-95a6ca1be9a6)

