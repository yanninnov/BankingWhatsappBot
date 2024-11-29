using Google.Cloud.Dialogflow.V2;
using Parameters = Google.Protobuf.WellKnownTypes.Struct;

namespace BankingChatbotWebAPI;

public interface IIntentProcessingAction
{
    public Task<WebhookResponse> ExecuteAsync(string session, Parameters parameters);
}