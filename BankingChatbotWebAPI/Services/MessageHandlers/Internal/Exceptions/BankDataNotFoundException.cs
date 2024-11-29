using System;
namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal.Exceptions
{
	public class BankDataNotFoundException: InternalConversationalEngineException
    {
		public BankDataNotFoundException(string message, InternalSessionState? nextState) : base(message, nextState)
        {
		}
	}
}

