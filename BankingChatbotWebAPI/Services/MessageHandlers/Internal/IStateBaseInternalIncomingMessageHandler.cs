namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal;


public interface IStateBasedInternalComingMessageHandler
{
    InternalSessionState State { get; }

    Task<(string Message, InternalSessionState NextState)> Handle(string sessionId, string text);
}
