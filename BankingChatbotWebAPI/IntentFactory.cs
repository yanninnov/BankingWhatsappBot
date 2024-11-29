using BankingChatbotWebAPI.Models;

namespace BankingChatbotWebAPI;

public interface IIntentFactory
{
    public IDictionary<string, Intent> GetIntents();
}

public class IntentFactory : IIntentFactory
{
    public IDictionary<string, Intent> GetIntents()
    {
        return new Dictionary<string, Intent>();
    }
}