using System.Net.Mail;
using System.Threading.Tasks;

namespace AppStore.Infra.CrossCutting.Notification
{
    public class SmtpClientProxy : ISmtpClient
    {
        private readonly SmtpClient _smtpClient;

        public SmtpClientProxy()
        {
            _smtpClient = new SmtpClient();
        }

        #region ISmtpClient Members

        public virtual void Dispose() => _smtpClient.Dispose();

        public void Send(MailMessage message) => _smtpClient.Send(message);

        public async Task SendAsync(MailMessage message, object userToken)
        {
            using (this._smtpClient)
            {
                await Task.Run(() => _smtpClient.SendAsync(message, userToken));
            };
        }

        #endregion
    }


}
