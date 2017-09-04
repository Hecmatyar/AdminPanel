using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using test.Models;
using System.Configuration;
using System.Net.Configuration;
using System.Net;

namespace test.Letters
{
    public class SendingLetters : Controllers.ControllerBase
    {
        public SmtpSection smtpSection;
        public SmtpClient smtpClient;
        public SendingLetters()
        {
            smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            smtpClient = new SmtpClient(smtpSection.Network.Host, smtpSection.Network.Port)
            {
                Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password)
            };
        }
        public void SendConfirmMail(LetterConfirmViewModel confirm, ControllerContext context)
        {
            MailMessage m = new MailMessage(
                               new MailAddress(smtpSection.From, "Web Registration"),
                               new MailAddress(confirm.UserEmail))
            {
                Subject = "Email confirmation",
                Body = RenderRazorViewToString("Letter/ConfirmRegistrationView", confirm, context),
                IsBodyHtml = true
            };
            smtpClient.Send(m);
        }
        public void SendResetPasswordMail(LetterResetPasswordViewModel reset, ControllerContext context)
        {
            MailMessage m = new MailMessage(
                               new MailAddress(smtpSection.From, "Web Registration"),
                               new MailAddress(reset.UserEmail))
            {
                Subject = "Email confirmation",
                Body = RenderRazorViewToString("Letter/ResetPasswordView", reset, context),
                IsBodyHtml = true
            };
            smtpClient.Send(m);
        }
    }
}