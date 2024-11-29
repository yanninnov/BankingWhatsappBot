namespace BankingChatbotWebAPI.Services.MessageHandlers.NLP;

public interface INLPIncomingMessageHandler: IIncomingMessageHandler
{
    Task<string> DetectIntentAsync(string sessionId, string text);
}
