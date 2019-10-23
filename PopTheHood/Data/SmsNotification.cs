using Microsoft.Extensions.Configuration;
using Nexmo.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Nexmo.Api.SMS;

namespace PopTheHood.Data
{
    public class SmsNotification
    {
        //public static string MessageApikey()
        //{
        //    IConfigurationBuilder builder = new ConfigurationBuilder();
        //    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

        //    IConfigurationRoot configuration = builder.Build();
        //    var EmailKey = configuration.GetSection("SMSAPIKey").GetSection("Key").Value;

        //    return EmailKey;
        //}

        //public static string SendMessage(string PhoneOrEmail, string OTP)
        //{
        //    IConfigurationBuilder builder = new ConfigurationBuilder();
        //    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
        //    IConfigurationRoot configuration = builder.Build();
        //    var SMSKey = configuration.GetSection("SMSAPIKey").GetSection("Key").Value;
        //    var SMSVal = configuration.GetSection("SMSAPIKey").GetSection("Value").Value;

        //    var client = new Client(SMSVal, SMSKey);

        //    var link = client.SendMessage(OTP, PhoneOrEmail);
        //    var result = "";
        //    if (link.Success)
        //    {
        //        result = "Message has been successfully sent";
        //    }
        //    else
        //    {
        //        result = link.ClientException.Message;
        //    }
        //    return result;
        //}


        //public static string SendMessage(string to, string text)
        //{
        //    var results = SMS.Send(new SMS.SMSRequest
        //    {
        //        from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
        //        to = to,
        //        text = text
        //    });

        //    return results.ToString();
        //}

       
        public static SMSResponse SendMessage(string to, string text) //List<Attachments> attachments, string body, string cc,
        {
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

                IConfigurationRoot configuration = builder.Build();
                var SMSKey = configuration.GetSection("appSettings").GetSection("Nexmo.api_key").Value;
                var SMSValue = configuration.GetSection("appSettings").GetSection("Nexmo.api_secret").Value;

                var client = new Client(creds: new Nexmo.Api.Request.Credentials
                {
                    //ApiKey = SMSKey,
                    //ApiSecret = SMSValue
                    ApiKey = "5d5eb59f",
                    ApiSecret = "xFT1BuHaxN6wzA8M"
                });

                var results = client.SMS.Send(new SMS.SMSRequest
                {
                    from = "19565390371",             //"7708178085",     //Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
                    to = "+14087224019",
                    text = text
                });

                //var res = results.messages;
                ////SMS.SMSResponseDetail rd = new SMS.SMSResponseDetail(results);
                ////rd = results;
                ////rd.status.ToString();

                return results;
            }

            catch (Exception e)
            {
                throw e;
            }

        }



        //public static List<SMSResponseDetail> SendMessage(string to, string text) //List<Attachments> attachments, string body, string cc,
        //{
        //    IConfigurationBuilder builder = new ConfigurationBuilder();
        //    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

        //    IConfigurationRoot configuration = builder.Build();
        //    var SMSKey = configuration.GetSection("appSettings").GetSection("Nexmo.api_key").Value;
        //    var SMSValue = configuration.GetSection("appSettings").GetSection("Nexmo.api_secret").Value;

        //    var client = new Client(creds: new Nexmo.Api.Request.Credentials
        //    {
        //        ApiKey = SMSKey,
        //        ApiSecret = SMSValue
        //    });

        //    var results = client.SMS.Send(new SMS.SMSRequest
        //    {
        //        from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
        //        to = to,
        //        text = text
        //    });

        //    List<SMSResponseDetail> res = results.messages.ToList();

        //    //SMSResponse sr = new SMSResponse();
        //    //sr = results.ToList();
        //    return res;

        //}

    }
}
