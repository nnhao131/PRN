using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Helper
{
    public interface IEmailService
    {
        Task SendMail(MailContent mailContent);
        Task SendEmailAsync(string email, string subject, string message);
    }
}
