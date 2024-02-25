using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Utils;

namespace AuthWebApi.Service;

public static class EmailService
{
    public static async Task SendEmailAsync(string email, string message)
    {
        var builder = new BodyBuilder ();
        var image = builder.LinkedResources.Add ("wwwroot/psp.png");

        image.ContentId = MimeUtils.GenerateMessageId ();

        builder.HtmlBody = $"<div style=\"background-color: white; padding-top: 50px; padding-bottom: 50px;\"> " +
                                 $"<div style=\"margin-left: auto; margin-right: auto; width: 400px; background-color: white; height: 400px; box-shadow: 0 2px 5px 0 rgb(0 0 0 / 10%), 0 2px 10px 0 rgb(0 0 0 / 10%);\"> " +
                                     $"<div style=\"margin-left: auto; margin-right: auto; width: 250px\"> " +
                                        $"<img style=\"margin-left: auto; margin-right: auto; width: 250px; margin-top: 20px\" src=\"cid:{image.ContentId}\"> " +
                                     $"</div> " +
                                     $"<div style=\"margin-left: auto; margin-right: auto; width: 105px; margin-top: 50px\">  " +
                                        $"<a href=\"{message}\" style=\"margin-left: auto; margin-right: auto; width: 100px; text-decoration: none; font-weight: 600; color: #fff; background-color: #0153A0; border-color: #0153A0; padding: 10px; border-radius: 5px\">Подтвердить</a>" +
                                     $"</div> " +
                                 $"</div> " +
                             $"</div>";
        
        var emailMessage = new MimeMessage();
 
        emailMessage.From.Add(MailboxAddress.Parse("lIdiotl@yandex.ru"));
        emailMessage.To.Add(MailboxAddress.Parse(email));
        emailMessage.Subject = "Активация аккаунта";
        emailMessage.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.yandex.ru", 25,  SecureSocketOptions.StartTls);
        await client.AuthenticateAsync("lIdiotl@yandex.ru", "psiorynxdknafems");
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}