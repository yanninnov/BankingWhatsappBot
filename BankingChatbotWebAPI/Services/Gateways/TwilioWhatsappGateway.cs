using Twilio;
using Twilio.TwiML;
using Twilio.Types;

namespace BankingChatbotWebAPI.Services.Gateways;

public class TwilioWhatsappGateway : IWhatsappGateway
{
    public string FormatResponse(string message)
    {
        var twiml = new MessagingResponse();
        twiml.Message(message);

        return twiml.ToString();
    }
}

public record TwilioSettings(string AccountSid, string AuthToken, string PhoneNumber)
{}