using System;
namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal.Exceptions;

public class InternalConversationalEngineException: Exception
{
	public InternalSessionState? NextState  { get; }

	public InternalConversationalEngineException(string message, InternalSessionState? nextState): base(message)
	{
		NextState = nextState;
	}
}

