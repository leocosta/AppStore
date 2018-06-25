using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AppStore.Infra.CrossCutting.Notification
{
    public interface ISmtpClient : IDisposable
    {
        void Send(MailMessage mesage);
        Task SendAsync(MailMessage mesage, object userToken);
    }
}
