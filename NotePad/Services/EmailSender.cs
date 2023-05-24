using MailKit.Net.Smtp;
using MimeKit;
using NotePad.Models;
using NotePad.Repositories;

namespace NotePad.Services
{
    public class EmailSender:IMessageSender
    {
        void IMessageSender.SendMessageSmtp(string email,string username,int Id)
        {
            var emailViewModel = new EmailViewModel
            {
                Id = Id
            };

            string builder = string.Format(@"
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='UTF-8'>
        <title>Confirm Your Account</title>
        <style>
            /* Reset styles */
            body {{
                margin: 0;
                padding: 0;
                font-family: Arial, sans-serif;
                line-height: 1.5;
                color: #333333;
            }}
            /* Main styles */
            .container {{
                max-width: 600px;
                margin: 0 auto;
                padding: 40px 20px;
                background-color: #f7f7f7;
                text-align: center;
            }}

            h1 {{
                margin-top: 0;
                font-size: 28px;
                font-weight: bold;
            }}

            p {{
                margin: 20px 0;
                font-size: 16px;
                line-height: 1.6;
            }}

            a {{
                display: inline-block;
                padding: 10px 20px;
                margin-top: 30px;
                background-color: #007bff;
                color: #ffffff !important;
                text-decoration: none;
                font-weight: bold;
                border-radius: 5px;
            }}

            a:hover {{
                background-color: #0062cc;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h1>Confirm Your Account</h1>
            <p>Thank you for signing up for our service! To complete your registration, please click the button below to confirm your account.</p>
           <a href='http://localhost:5259/account/Active_User?UserId={0}'>Confirm Account</a>
        </div>
    </body>
    </html>", emailViewModel.Id);
            //if You want send link please locate the href attribute correctly
            // Compose a message
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("NotePad", "ibo@admin-ibo-bodyguards.co.uk"));
            mail.To.Add(new MailboxAddress(username, email));
            mail.Subject = "Register";
            mail.Body = new TextPart("html")
            {
                Text = builder
            };
            // Send it!
            using (var client = new SmtpClient())
            {
                // XXX - Should this be a little different?
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("mail.admin-ibo-bodyguards.co.uk", 587);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("ibo@admin-ibo-bodyguards.co.uk", "Fard1234");
                client.Send(mail);
                client.Disconnect(true);

            }

        }

    }
}