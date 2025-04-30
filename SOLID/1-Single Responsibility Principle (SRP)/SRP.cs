using SOLID.Single_Responsibility_Principle__SRP_.Problem;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace SOLID.Single_Responsibility_Principle__SRP_
{
    namespace Problem
    {
        public class UserService
        {
            private SmtpClient _smtpClient;

            public void Register(string email, string password)
            {
                if (!ValidateEmail(email))
                    throw new ValidationException("Email is not an email");
                var user = new User(email, password);

                SendEmail(new MailMessage("mysite@nowhere.com", email) { Subject = "HEllo foo" });
            }

            public virtual bool ValidateEmail(string email)
            {
                return email.Contains("@");
            }

            public bool SendEmail(MailMessage message)
            {
                _smtpClient.Send(message);
                return true;
            }
        }
        public class User
        {
            public User(string email, string password)
            {
                throw new NotImplementedException();
            }
        }
    }

    namespace Solution
    {
        public class UserService
        {
            EmailService _emailService;
            DbContext _dbContext;
            public UserService(EmailService aEmailService, DbContext aDbContext)
            {
                _emailService = aEmailService;
                _dbContext = aDbContext;
            }
            public void Register(string email, string password)
            {
                if (!_emailService.ValidateEmail(email))
                    throw new ValidationException("Email is not an email");
                var user = new User(email, password);
                // add user to entity
                _dbContext.SaveChanges();
                _emailService.SendEmail(new MailMessage("myname@mydomain.com", email) { Subject = "Hi. How are you!" });

            }
        }
        public class EmailService
        {
            System.Net.Mail.SmtpClient _smtpClient;
            public EmailService(SmtpClient aSmtpClient)
            {
                _smtpClient = aSmtpClient;
            }
            public virtual bool ValidateEmail(string email)
            {
                return email.Contains("@");
            }
            public bool SendEmail(MailMessage message)
            {
                _smtpClient.Send(message);
                return true;
            }
        }

    }

}
