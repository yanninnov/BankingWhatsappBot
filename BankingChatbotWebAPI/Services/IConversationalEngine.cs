namespace BankingChatbotWebAPI.Services;

public interface IConversationalEngine
{
    Task<string> ProcessUserInput(string sessionId, string text);
}
