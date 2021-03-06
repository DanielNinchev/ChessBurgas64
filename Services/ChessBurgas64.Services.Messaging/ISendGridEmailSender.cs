namespace ChessBurgas64.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISendGridEmailSender
    {
        Task SendEmailAsync(
            string from,
            string fromName,
            string to,
            string subject,
            string htmlContent,
            IEnumerable<EmailAttachment> attachments = null);
    }
}
