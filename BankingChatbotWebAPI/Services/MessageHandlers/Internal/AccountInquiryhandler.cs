using System;
using System.Security.Principal;
using BankingChatbotWebAPI.Services.Connector.Bank.Models;
using BankingChatbotWebAPI.Services.MessageHandlers.Internal.Exceptions;

namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal
{
    public class AccountInquiryHandler : IStateBasedInternalComingMessageHandler
    {
        public InternalSessionState State => InternalSessionState.ACCOUNT_INQUIRY_PRINCIPAL_MENU;

        private readonly ICBSService _bankService;

        public AccountInquiryHandler(ICBSService bankService)
        {
            _bankService = bankService;
        }

        public async Task<(string Message, InternalSessionState NextState)> Handle(string sessionId, string text)
        {
            return text switch
            {
                "1" => (InternalReturnMessage.ACCOUNT_INQUIRY_CHOOSE_ACCOUNT, InternalSessionState.ACCOUNT_INQUIRY_BALANCE_CHOOSE_ACCOUNT_MENU),
                "2" => (InternalReturnMessage.ACCOUNT_INQUIRY_CHOOSE_ACCOUNT, InternalSessionState.ACCOUNT_INQUIRY_RIB_CHOOSE_ACCOUNT_MENU),
                "3" => (await GetAccountManagerDetails(sessionId), InternalSessionState.GREETING),
                "4" => (InternalReturnMessage.ACCOUNT_INQUIRY_CHOOSE_ACCOUNT, InternalSessionState.ACCOUNT_INQUIRY_HISTORY_CHOOSE_ACCOUNT_MENU),
                _ => (InternalReturnMessage.DEFAULT_NO_MATCH_MESSAGE, InternalSessionState.ACCOUNT_INQUIRY_PRINCIPAL_MENU)
            };
        }

        private async Task<string> GetAccountManagerDetails(string sessionId)
        {
            var accountManager = await _bankService.GetAccountManager(sessionId);

            if (accountManager is null)
            {
                throw new BankDataNotFoundException($"No account manager details found for sessionId {sessionId}", null);
            }

            return ProcessTemplate(InternalReturnMessage.ACCOUNT_MANAGER_DETAILS_TEMPLATE, accountManager);
        }


        private string ProcessTemplate(string template, AccountManager manager)
        {
            return template.Replace($"@{nameof(manager.Fullname)}", manager.Fullname)
                    .Replace($"@{nameof(manager.Address)}", manager.Address)
                    .Replace($"@{nameof(manager.Contact)}", manager.Contact)
                    .Replace($"@{nameof(manager.Whatsapp)}", manager.Whatsapp);
        }
    }
}
