using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Data
{
    public class EmailSendGrid
    {
        public static string Apikey()  //Login userInfo
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            IConfigurationRoot configuration = builder.Build();
            var EmailKey = configuration.GetSection("EmailAPIKey").GetSection("Key").Value;

            return EmailKey;
        }


        public static async Task<string> Mail(string from, string to, string subject, string UserName, string BodyContent)//, string FilePath) //List<Attachments> attachments, string body, string cc,
        {
            try
            { 
                //var owners = System.IO.File.ReadAllLines(@"..\wwwroot\EmailTemplate\ErrorMessageNotification.html");
                var client = new SendGridClient(Apikey());
                SendGridMessage mail = new SendGridMessage();

                #region EmailTemplate for Content of the Mail
                string Body = string.Empty;

                //using (System.IO.StreamReader sr = new System.IO.StreamReader(FilePath))
                //{
                //    Body = sr.ReadToEnd();
                //}
                //Body = Body.Replace("{{name}}", string.Join(" / ", to));
                //Body = Body.Replace("{{LogoImage}}", ImagePath);
                //Body = Body.Replace("{{CompanyName}}", "PopTheHood");
                //Body = Body.Replace("{{From}}",from);
                //Body = Body.Replace("{{To}}", to);
                //Body = Body.Replace("{{UserName}}", UserName);
                //Body = Body.Replace("{{BodyContent}}", BodyContent); 

                mail.HtmlContent = Body;
                #endregion

                mail.From = new EmailAddress(from);
               // mail.SetFrom(new EmailAddress("murukeshs@apptomate.co", "No Reply"));
            
                if (to != null)
                {
                    //foreach (string to1 in to)
                    //{
                     mail.AddTo(to);
                    //mail.AddTo(new EmailAddress(to));
                    //}
                }
                //if (cc != null)
                //{
                //    //foreach (string cc1 in cc)
                //        mail.AddCc(cc);
                //}

                mail.Subject = subject;
                mail.PlainTextContent = BodyContent;

                //if (attachments != null)
                //{
                //    foreach (var res in attachments)
                //    {
                //        String bytes = Convert.ToBase64String(res.file);
                //        mail.AddAttachment(res.filename + "." + res.contenttype, bytes, "Text/" + res.contenttype);
                //    }
                //}

                var status = await client.SendEmailAsync(mail);
                return status.StatusCode.ToString();
            }

            catch (Exception e)
            {
                throw e;
            }

        }


    }
}
