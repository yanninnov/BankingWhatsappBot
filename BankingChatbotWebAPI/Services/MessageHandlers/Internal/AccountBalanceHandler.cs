using System;
using System.Linq;
using System.Collections.Generic;
using BankingChatbotWebAPI.Services.Connector.Bank.Exceptions;
using BankingChatbotWebAPI.Services.MessageHandlers.Internal.Exceptions;
using BankingChatbotWebAPI.Services.Connector.Bank.Models;

namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal
{
    public class AccountBalanceHandler : IStateBasedInternalComingMessageHandler
    {
        private readonly ICBSService _bankService;

        public AccountBalanceHandler(ICBSService bankService)
        {
            _bankService = bankService;
        }

        public InternalSessionState State => InternalSessionState.ACCOUNT_INQUIRY_BALANCE_CHOOSE_ACCOUNT_MENU;

        public async Task<(string Message, InternalSessionState NextState)> Handle(string sessionId, string text)
        {
            if (text is null)
            {
                throw new WrongUserInputException(nameof(InternalSessionState.ACCOUNT_INQUIRY_BALANCE_CHOOSE_ACCOUNT_MENU));
            }

            return await GetAccountBalance(sessionId, text);
        }

        private async Task<(string Message, InternalSessionState NextState)> GetAccountBalance(string sessionId, string account)
        {
            var balance = await _bankService.GetAccountBalance(sessionId, account);

            if (balance is null)
            {
                throw new BankDataNotFoundException($"No balance details found for Account {account} and sessionId {sessionId}", null);
            }

            return (ProcessTemplate(InternalReturnMessage.ACCOUNT_BALANCE_TEMPLATE, balance), InternalSessionState.GREETING);
        }

        private string ProcessTemplate(string template, AccountBalance balance)
        {
            return template.Replace($"@{nameof(balance.Amount)}", balance.Amount)
                    .Replace($"@{nameof(balance.Currency)}", balance.Currency)
                    .Replace($"@{nameof(balance.From)}", balance.From);
        }
    }
}
