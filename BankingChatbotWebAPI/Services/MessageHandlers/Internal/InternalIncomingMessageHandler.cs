using System.Collections.Concurrent;

namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal;

public class InternalIncomingMessageHandler : IIncomingMessageHandler
{
    private readonly ConcurrentDictionary<string, (InternalSessionState State, DateTime LastActivity)> _sessionStates = new();
    private readonly IReadOnlyCollection<IStateBasedInternalComingMessageHandler> _incomingMessagesHandlers;
    private static readonly TimeSpan SessionTimeout = TimeSpan.FromSeconds(30);

    public InternalIncomingMessageHandler(IEnumerable<IStateBasedInternalComingMessageHandler> incomingMessagesHandlers)
    {
        _incomingMessagesHandlers = incomingMessagesHandlers?.ToList() 
            ?? throw new ArgumentNullException(nameof(incomingMessagesHandlers));
    }

    public async Task<string> HandleMessage(string sessionId, string text)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            throw new ArgumentException("Session ID cannot be null or empty.", nameof(sessionId));
        }

        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("Text cannot be null or empty.", nameof(text));
        }

        // Set initial state if it doesn't exist
        _sessionStates.AddOrUpdate(sessionId,
            _ => (InternalSessionState.GREETING, DateTime.UtcNow),
            (_, existing) => (existing.State, DateTime.UtcNow));

        //Get current state and find handler for that state       
        var currentState = _sessionStates[sessionId];

        // Find a handler for the current state
        var handler = _incomingMessagesHandlers.FirstOrDefault(h => h.State == currentState.State);
        if (handler == null)
        {
            return InternalReturnMessage.DEFAULT_NO_MATCH_MESSAGE;
        }

        //Handle user input and set next state
        var response = await handler.Handle(sessionId, text);
        _sessionStates[sessionId] = (response.NextState, DateTime.UtcNow);

        return response.Message;
    }

    /*public void CleanUpSessions()
    {
        var now = DateTime.UtcNow;
        var expiredSessions = _sessionStates
            .Where(kv => now - kv.Value.LastActivity > SessionTimeout)
            .Select(kv => kv.Key)
            .ToList();

        foreach (var sessionId in expiredSessions)
        {
            _sessionStates.Remove(sessionId, out _);
        }

        Console.WriteLine($"Sessions cleaned: {expiredSessions.Count} sessions removed.");
    }*/
}
