using System;

namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal
{
	public class GreetingHandler : IStateBasedInternalComingMessageHandler
	{
		public GreetingHandler()
		{
		}

        public InternalSessionState State => InternalSessionState.GREETING;


        public async Task<(string Message, InternalSessionState NextState)> Handle(string sessionId, string text)
        {
            return (InternalReturnMessage.WELCOME_MESSAGE, InternalSessionState.PRINCIPAL_MENU);
        }
    }
}

