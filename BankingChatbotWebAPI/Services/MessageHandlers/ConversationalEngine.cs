namespace BankingChatbotWebAPI.Services.MessageHandlers;

public class ConversationalEngine: IConversationalEngine
{
    private IIncomingMessageHandler _baseIncomingMessageHandler;
    
    public ConversationalEngine(IIncomingMessageHandler baseIncomingMessageHandler)
    {
        _baseIncomingMessageHandler = baseIncomingMessageHandler;
    }

    public async Task<string> ProcessUserInput(string sessionId, string text)
    {
        return await _baseIncomingMessageHandler.HandleMessage(sessionId, text);
    }
}
