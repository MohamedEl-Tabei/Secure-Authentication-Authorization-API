using BL.Extensions;
using BL.IManagers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Vonage;
using Vonage.Request;
namespace BL.Managers
{
    public class SMSManager : ISMSManager
    {
        private readonly IConfiguration _configuration;

        public SMSManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> SendVerificationCode(string phoneNumber)
        {
            var credentials = Credentials.FromApiKeyAndSecret(
                _configuration["Vonage:API:Key"],
                _configuration["Vonage:API:Secret"]

                );

            var VonageClient = new VonageClient(credentials);
            var code = Guid.NewGuid().ToString().Substring(0, 4);

            //var response = await VonageClient.SmsClient.SendAnSmsAsync(new Vonage.Messaging.SendSmsRequest()
            //{
            //    To = phoneNumber,
            //    From = "Secure-Authentication-Authorization",
            //    Text = $"Your verification code is {code}. It will expire in 5 minutes."
            //});
            return code.Hash(phoneNumber, _configuration["Hash:Key"]);
        }

    }
}
