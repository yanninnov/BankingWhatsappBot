namespace BankingChatbotWebAPI.Services.MessageHandlers;

public interface IIncomingMessageHandler
{
    Task<string> HandleMessage(string sessionId, string text);

}
