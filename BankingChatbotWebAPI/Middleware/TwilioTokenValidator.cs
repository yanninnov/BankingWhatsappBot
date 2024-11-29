using System;
using Twilio.Security;

namespace BankingChatbotWebAPI.Middleware
{
	public class TwilioTokenValidator
	{
        public bool ValidateTwilioRequest(HttpRequest request)
        {
            string authToken = "Your_Twilio_Auth_Token";
            string twilioSignature = request.Headers["X-Twilio-Signature"];
            string url = request.Path.ToString();

            // Collect request form parameters
            var formParameters = request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

            var validator = new RequestValidator(authToken);
            return validator.Validate(url, formParameters, twilioSignature);
        }
    }
}