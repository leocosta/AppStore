using System;
using AppStore.Infra.CrossCutting.Notifications;
using System.Net.Mail;
using AppStore.Infra.CrossCutting.Logging;

namespace AppStore.Infra.CrossCutting.Notification
{
    public class MailService : IMessageService
    {
        private readonly ISmtpClient _smtpClient;

        public MailService()
        {
            _smtpClient = new SmtpClientProxy();
        }

        public void Send(string to, string subject, string body)
        {
            if (to == null)
                throw new ArgumentNullException("to");

            if (subject == null)
                throw new ArgumentNullException("subject");

            if (body == null)
                throw new ArgumentNullException("body");

            using (var message = new MailMessage())
            {
                message.From = new MailAddress("from@domain.com");
                message.Subject = subject;
                message.Body = body;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;

                try
                {
                    _smtpClient.Send(message);
                }
                catch (SmtpException ex)
                {
                    Logger.Error("MailService: {0}", ex.Message.ToString());

                    throw new NotificationException(ex.Message);
                }
            }
        }
    }
}