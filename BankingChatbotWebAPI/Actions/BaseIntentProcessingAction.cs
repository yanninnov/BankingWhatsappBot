using Google.Cloud.Dialogflow.V2;
using Parameters = Google.Protobuf.WellKnownTypes.Struct;

namespace BankingChatbotWebAPI.Actions;

public abstract class BaseIntentProcessingAction : IIntentProcessingAction
{
    public abstract Task<WebhookResponse> ExecuteAsync(string session, Parameters parameters);

    protected string GetParameter(Parameters pas, string field){
        return pas.Fields.ContainsKey("name") ? pas.Fields["name"].ToString().Replace('\"', ' ').Trim() : string.Empty;
    }
}