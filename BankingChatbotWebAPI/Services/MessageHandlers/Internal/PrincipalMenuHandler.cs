using System;

namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal
{
	public class PrincipalMenuHandler : IStateBasedInternalComingMessageHandler
	{
        public InternalSessionState State => InternalSessionState.PRINCIPAL_MENU;

        public async Task<(string Message, InternalSessionState NextState)> Handle(string sessionId, string text)
        {
            return text switch
            {
                "1" => (InternalReturnMessage.PRINCIPAL_MENU_ACCOUNT_INQUIRY_CHOICES, InternalSessionState.ACCOUNT_INQUIRY_PRINCIPAL_MENU),
                "2" => (InternalReturnMessage.PRINCIPAL_MENU_TRANSACTION_CHOICES, InternalSessionState.TRANSACTION_PRINCIPAL_MENU),
                "3" => (InternalReturnMessage.PRINCIPAL_MENU_SERVICE_CHOICES, InternalSessionState.SERVICE_PRINCIPAL_MENU),
                "4" => (InternalReturnMessage.PRINCIPAL_MENU_CLAIM_CHOICES, InternalSessionState.CLAIM_PRINCIPAL_MENU),
                "5" => (InternalReturnMessage.PRINCIPAL_MENU_BRANCH_CHOICES, InternalSessionState.BRANCH_PRINCIPAL_MENU),
                _ => (InternalReturnMessage.DEFAULT_NO_MATCH_MESSAGE, InternalSessionState.GREETING)
            };
        }
    }
}

