using System;
using BankingChatbotWebAPI.Services.Connector.Bank.Models;
using BankingChatbotWebAPI.Services.MessageHandlers.Internal.Exceptions;
using static Google.Rpc.Help.Types;

namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal
{
    public class AccountHistoryHandler : IStateBasedInternalComingMessageHandler
    {
        private readonly ICBSService _bankService;

        public AccountHistoryHandler(ICBSService bankService)
        {
            _bankService = bankService;
        }

        public InternalSessionState State => InternalSessionState.ACCOUNT_INQUIRY_HISTORY_CHOOSE_ACCOUNT_MENU;

        public async Task<(string Message, InternalSessionState NextState)> Handle(string sessionId, string text)
        {
            if (text is null)
            {
                throw new WrongUserInputException(nameof(InternalSessionState.ACCOUNT_INQUIRY_HISTORY_CHOOSE_ACCOUNT_MENU));
            }

            return await GetAccountHistory(sessionId, text);
        }

        private async Task<(string Message, InternalSessionState NextState)> GetAccountHistory(string sessionId, string account)
        {
            var history = await _bankService.GetAccountHistory(sessionId, account);

            if (history is null)
            {
                throw new BankDataNotFoundException($"No history details found for Account {account} and sessionId {sessionId}", null);
            }

            return (ProcessTemplate(InternalReturnMessage.ACCOUNT_HISTORY_TEMPLATE, history), InternalSessionState.GREETING);
        }

        private string ProcessTemplate(string template, IEnumerable<AccountHistoryLine> history)
        {
            var historyStringList = history.Select(line =>
                InternalReturnMessage.ACCOUNT_HISTORY_LINE_TEMPLATE.Replace($"@{nameof(line.Amount)}", line.Amount)
                    .Replace($"@{nameof(line.Currency)}", line.Currency)
                    .Replace($"@{nameof(line.Date)}", line.Date)
                    .Replace($"@{nameof(line.Description)}", line.Description)
                    .Replace($"@{nameof(line.Type)}", line.Type)).ToList();

            return InternalReturnMessage.ACCOUNT_HISTORY_TEMPLATE.Replace($"@AccountHistory", String.Join("", historyStringList));
        }
    }
}
