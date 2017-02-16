using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace movemate_api.Controllers
{
    public static class MailSender
    {
        public static void SendEmail(String email, String code)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("unimovemate@gmail.com", "tyqjqqoulizihbix"); // vera password su onedrive
            client.EnableSsl = true;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            MailMessage mail = new MailMessage("unimovemate@gmail.com", email);
            mail.Body = String.Concat("Benvenuto in MoveMate!\nPer continuare la fase di registrazione, inserisci il seguente codice nella tua applicazione:\n", code);
            mail.Subject = "MoveMate - Codice di verifica";
            client.Send(mail);

        }
    }
}