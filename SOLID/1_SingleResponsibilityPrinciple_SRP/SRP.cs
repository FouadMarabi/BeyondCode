using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using SOLID._1_SingleResponsibilityPrinciple_SRP.Problem;

namespace SOLID._1_SingleResponsibilityPrinciple_SRP
{
    // there should never be more than one reason for a class to change. In other words, every class should have only one responsibility.

    //Importance
    //Maintainability: When classes have a single, well-defined responsibility, they're easier to understand and modify.
    //Testability: It's easier to write unit tests for classes with a single focus.
    //Flexibility: Changes to one responsibility don't affect unrelated parts of the system.

    namespace Problem
    {
        public class UserService
        {
            private SmtpClient _smtpClient;

            public UserService(SmtpClient smtpClient)
            {
                _smtpClient = smtpClient;
            }

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
