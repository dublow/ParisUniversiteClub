using System.Collections.Generic;

namespace web.models
{
    public class EmailModel
    {
        public int[] RegisterIds { get; set; }
    }

    public class EmailViewModel
    {
        public string Emails { get; }

        public EmailViewModel(string emails)
        {
            Emails = emails;
        }
    }

    public class SendEmailModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Emails { get; set; }
    }
}
