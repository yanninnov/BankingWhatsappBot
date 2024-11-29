using Google.Cloud.Dialogflow.V2;

namespace BankingChatbotWebAPI.Services.MessageHandlers.NLP;

public class DialogFlowIncomingMessageHandler : INLPIncomingMessageHandler
{
    private readonly SessionsClient _sessionsClient;

    public DialogFlowIncomingMessageHandler(SessionsClient sessionsClient)
    {
        _sessionsClient = sessionsClient;
    }

    public async Task<string> HandleMessage(string sessionId, string text){
        var responseText = await DetectIntentAsync(sessionId, text);
        return responseText;
    }

    public async Task<string> DetectIntentAsync(string sessionId, string text)
    {
        var sessionName = new SessionName("your-project-id", sessionId); 

        var detectIntentRequest = new DetectIntentRequest
        {
            SessionAsSessionName = sessionName,
            QueryInput = new QueryInput
            {
                Text = new TextInput
                {
                    Text = text,
                    LanguageCode = "fr"
                }
            }
        };

        var response = await _sessionsClient.DetectIntentAsync(detectIntentRequest);
        return response.QueryResult.FulfillmentText;
    }
}
