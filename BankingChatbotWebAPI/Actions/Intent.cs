using BankingChatbotWebAPI;

namespace BankingChatbotWebAPI.Models;

public class Intent
{
    public string Name { get; init; }
    public bool RequiresPostProcessing { get; init; }
    public IIntentProcessingAction ProcessingAction { get; init; }

    public Intent(string name, bool requiresPostProcessing, IIntentProcessingAction processingAction)
    {
        this.Name = name;
        this.RequiresPostProcessing = requiresPostProcessing;   
        this.ProcessingAction = processingAction;

        if (requiresPostProcessing && processingAction is null)
        {
            throw new Exception("Intent require processing action");
        }
    }
}