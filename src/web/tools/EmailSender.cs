using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using web.extensions;

namespace web.tools
{
    public class EmailSender : IEmailSender
    {
        private readonly string _server;
        private readonly int _port;
        private readonly string _from;
        private readonly string _login;
        private readonly string _password;

        public EmailSender(string server, int port, string from, string login, string password)
        {
            _server = server;
            _port = port;
            _from = @from;
            _login = login;
            _password = password;
        }
        public void Send(string subject, string message, IEnumerable<string> emails)
        {
            var mailMessage = new MailMessage
            {
                Subject = subject,
                Body = message,
                From = new MailAddress(_from),

            };

            emails.Do(mailMessage.To.Add);

            using var smtpClient = new SmtpClient(_server, _port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_login, _password)
            };
            smtpClient.Send(mailMessage);
        }
    }

    public interface IEmailSender
    {
        void Send(string subject, string message, IEnumerable<string> emails);
    }
}
