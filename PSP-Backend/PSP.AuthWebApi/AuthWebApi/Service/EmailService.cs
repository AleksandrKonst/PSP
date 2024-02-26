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

        builder.HtmlBody = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n<html dir=\"ltr\" xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" lang=\"ru\">\n <head>\n  <meta charset=\"UTF-8\">\n  <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\">\n  <meta name=\"x-apple-disable-message-reformatting\">\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n  <meta content=\"telephone=no\" name=\"format-detection\">\n  <title>Empty template</title><!--[if (mso 16)]>\n    <style type=\"text/css\">\n    a {{text-decoration: none;}}\n    </style>\n    <![endif]--><!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]--><!--[if gte mso 9]>\n<xml>\n    <o:OfficeDocumentSettings>\n    <o:AllowPNG></o:AllowPNG>\n    <o:PixelsPerInch>96</o:PixelsPerInch>\n    </o:OfficeDocumentSettings>\n</xml>\n<![endif]-->\n  <style type=\"text/css\">\n.rollover:hover .rollover-first {{\n  max-height:0px!important;\n  display:none!important;\n  }}\n  .rollover:hover .rollover-second {{\n  max-height:none!important;\n  display:block!important;\n  }}\n  .rollover span {{\n  font-size:0px;\n  }}\n  u + .body img ~ div div {{\n  display:none;\n  }}\n  #outlook a {{\n  padding:0;\n  }}\n  span.MsoHyperlink,\nspan.MsoHyperlinkFollowed {{\n  color:inherit;\n  mso-style-priority:99;\n  }}\n  a.es-button {{\n  mso-style-priority:100!important;\n  text-decoration:none!important;\n  }}\n  a[x-apple-data-detectors] {{\n  color:inherit!important;\n  text-decoration:none!important;\n  font-size:inherit!important;\n  font-family:inherit!important;\n  font-weight:inherit!important;\n  line-height:inherit!important;\n  }}\n  .es-desk-hidden {{\n  display:none;\n  float:left;\n  overflow:hidden;\n  width:0;\n  max-height:0;\n  line-height:0;\n  mso-hide:all;\n  }}\n  .es-button-border:hover > a.es-button {{\n  color:#ffffff!important;\n  }}\n@media only screen and (max-width:600px) {{*[class=\"gmail-fix\"] {{ display:none!important }} p, a {{ line-height:150%!important }} h1, h1 a {{ line-height:120%!important }} h2, h2 a {{ line-height:120%!important }} h3, h3 a {{ line-height:120%!important }} h4, h4 a {{ line-height:120%!important }} h5, h5 a {{ line-height:120%!important }} h6, h6 a {{ line-height:120%!important }} h1 {{ font-size:30px!important; text-align:left }} h2 {{ font-size:24px!important; text-align:left }} h3 {{ font-size:20px!important; text-align:left }} h4 {{ font-size:24px!important; text-align:left }} h5 {{ font-size:20px!important; text-align:left }} h6 {{ font-size:16px!important; text-align:left }} .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {{ font-size:30px!important }} .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {{ font-size:24px!important }} .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {{ font-size:20px!important }} .es-header-body h4 a, .es-content-body h4 a, .es-footer-body h4 a {{ font-size:24px!important }} .es-header-body h5 a, .es-content-body h5 a, .es-footer-body h5 a {{ font-size:20px!important }} .es-header-body h6 a, .es-content-body h6 a, .es-footer-body h6 a {{ font-size:16px!important }} .es-menu td a {{ font-size:14px!important }} .es-header-body p, .es-header-body a {{ font-size:14px!important }} .es-content-body p, .es-content-body a {{ font-size:14px!important }} .es-footer-body p, .es-footer-body a {{ font-size:14px!important }} .es-infoblock p, .es-infoblock a {{ font-size:12px!important }} .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3, .es-m-txt-c h4, .es-m-txt-c h5, .es-m-txt-c h6 {{ text-align:center!important }} .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3, .es-m-txt-r h4, .es-m-txt-r h5, .es-m-txt-r h6 {{ text-align:right!important }} .es-m-txt-j, .es-m-txt-j h1, .es-m-txt-j h2, .es-m-txt-j h3, .es-m-txt-j h4, .es-m-txt-j h5, .es-m-txt-j h6 {{ text-align:justify!important }} .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3, .es-m-txt-l h4, .es-m-txt-l h5, .es-m-txt-l h6 {{ text-align:left!important }} .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {{ display:inline!important }} .es-m-txt-r .rollover:hover .rollover-second, .es-m-txt-c .rollover:hover .rollover-second, .es-m-txt-l .rollover:hover .rollover-second {{ display:inline!important }} .es-m-txt-r .rollover span, .es-m-txt-c .rollover span, .es-m-txt-l .rollover span {{ line-height:0!important; font-size:0!important }} .es-spacer {{ display:inline-table }} a.es-button, button.es-button {{ font-size:18px!important; line-height:120%!important }} a.es-button, button.es-button, .es-button-border {{ display:inline-block!important }} .es-m-fw, .es-m-fw.es-fw, .es-m-fw .es-button {{ display:block!important }} .es-m-il, .es-m-il .es-button, .es-social, .es-social td, .es-menu {{ display:inline-block!important }} .es-adaptive table, .es-left, .es-right {{ width:100%!important }} .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {{ width:100%!important; max-width:600px!important }} .adapt-img {{ width:100%!important; height:auto!important }} .es-mobile-hidden, .es-hidden {{ display:none!important }} .es-desk-hidden {{ width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important }} tr.es-desk-hidden {{ display:table-row!important }} table.es-desk-hidden {{ display:table!important }} td.es-desk-menu-hidden {{ display:table-cell!important }} .es-menu td {{ width:1%!important }} table.es-table-not-adapt, .esd-block-html table {{ width:auto!important }} .es-social td {{ padding-bottom:10px }} .h-auto {{ height:auto!important }} }}\n@media screen and (max-width:384px) {{.mail-message-content {{ width:414px!important }} }}\n</style>\n </head>\n <body class=\"body\" style=\"width:100%;height:100%;padding:0;Margin:0\">\n  <div dir=\"ltr\" class=\"es-wrapper-color\" lang=\"ru\" style=\"background-color:#F6F6F6\"><!--[if gte mso 9]>\n\t\t\t<v:background xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"t\">\n\t\t\t\t<v:fill type=\"tile\" color=\"#f6f6f6\"></v:fill>\n\t\t\t</v:background>\n\t\t<![endif]-->\n   <table class=\"es-wrapper\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#F6F6F6\">\n     <tr>\n      <td valign=\"top\" style=\"padding:0;Margin:0\">\n       <table class=\"es-content\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important\">\n         <tr>\n          <td align=\"center\" style=\"padding:0;Margin:0\">\n           <table class=\"es-content-body\" cellspacing=\"0\" cellpadding=\"0\" bgcolor=\"#ffffff\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\n             <tr>\n              <td align=\"left\" style=\"padding:0;Margin:0;padding-top:20px;padding-right:20px;padding-left:20px\">\n               <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\n                 <tr>\n                  <td valign=\"top\" align=\"center\" style=\"padding:0;Margin:0;width:560px\">\n                   <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\n                     <tr>\n                      <td align=\"center\" style=\"padding:0;Margin:0;font-size:0\"><img src=\"cid:{image.ContentId}\" alt=\"\" width=\"400\" class=\"adapt-img\" style=\"display:block;font-size:14px;border:0;outline:none;text-decoration:none\"></td>\n                     </tr>\n                   </table></td>\n                 </tr>\n               </table></td>\n             </tr>\n           </table></td>\n         </tr>\n       </table>\n       <table class=\"es-content\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important\">\n         <tr>\n          <td align=\"center\" bgcolor=\"transparent\" style=\"padding:0;Margin:0\">\n           <table class=\"es-content-body\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#ffffff\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px\">\n             <tr>\n              <td align=\"left\" style=\"padding:0;Margin:0;padding-top:20px;padding-right:20px;padding-left:20px\">\n               <table cellpadding=\"0\" cellspacing=\"0\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\n                 <tr>\n                  <td align=\"left\" style=\"padding:0;Margin:0;width:560px\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\n                     <tr>\n                      <td align=\"center\" style=\"padding:0;Margin:0;padding-top:20px;padding-bottom:20px\"><!--[if mso]><a href=\"{message}\" target=\"_blank\" hidden>\n\t<v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" esdevVmlButton href=\"{message}\" \n                style=\"height:39px; v-text-anchor:middle; width:103px\" arcsize=\"13%\" strokecolor=\"#0153a0\" strokeweight=\"2px\" fillcolor=\"#0153a0\">\n\t\t<w:anchorlock></w:anchorlock>\n\t\t<center style='color:#ffffff; font-family:arial, \"helvetica neue\", helvetica, sans-serif; font-size:14px; font-weight:400; line-height:14px;  mso-text-raise:1px'>Подтвердить</center>\n\t</v:roundrect></a>\n<![endif]--><!--[if !mso]><!-- --><span class=\"es-button-border msohide\" style=\"border-style:solid;border-color:#0153A0;background:#0153A0;border-width:0px 0px 2px 0px;display:inline-block;border-radius:5px;width:auto;mso-hide:all\"><a href=\"{message}\" class=\"es-button\" target=\"_blank\" style=\"mso-style-priority:100 !important;text-decoration:none !important;mso-line-height-rule:exactly;color:#FFFFFF;font-size:18px;padding:10px 20px 10px 20px;display:inline-block;background:#0153A0;border-radius:5px;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-weight:normal;font-style:normal;line-height:22px;width:auto;text-align:center;letter-spacing:0;mso-padding-alt:0;mso-border-alt:10px solid #0153A0\">Подтвердить</a></span><!--<![endif]--></td>\n                     </tr>\n                   </table></td>\n                 </tr>\n               </table></td>\n             </tr>\n           </table></td>\n         </tr>\n       </table>\n       <table class=\"es-content\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:100%;table-layout:fixed !important\">\n         <tr>\n          <td align=\"center\" style=\"padding:0;Margin:0\">\n           <table class=\"es-content-body\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#0153A0\" align=\"center\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#0153A0;width:600px\" role=\"none\">\n             <tr>\n              <td align=\"left\" style=\"padding:0;Margin:0;padding-top:20px;padding-right:20px;padding-left:20px\">\n               <table cellpadding=\"0\" cellspacing=\"0\" role=\"none\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\n                 <tr>\n                  <td align=\"left\" style=\"padding:0;Margin:0;width:560px\">\n                   <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" role=\"presentation\" style=\"mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px\">\n                     <tr>\n                      <td align=\"left\" style=\"padding:0;Margin:0;padding-bottom:20px\"><p style=\"Margin:0;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;letter-spacing:0;color:#fefdfd;font-size:14px\" align=\"left\">Приложению \"PSP\" будет предоставлен доступ к вашим данным: имени, адресу электронной почты. Прежде чем начать работу с приложением \"PSP\", вы можете ознакомиться с его политикой конфиденциальности и условиями использования.</p></td>\n                     </tr>\n                   </table></td>\n                 </tr>\n               </table></td>\n             </tr>\n           </table></td>\n         </tr>\n       </table></td>\n     </tr>\n   </table>\n  </div>\n </body>\n</html>";
        
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