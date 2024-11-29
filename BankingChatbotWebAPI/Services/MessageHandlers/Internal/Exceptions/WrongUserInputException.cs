using System;
namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal.Exceptions
{
	public class WrongUserInputException : InternalConversationalEngineException
    {
		public WrongUserInputException(string action) : base($"Wrong user input for {action}", null)
        {
		}
	}
}

