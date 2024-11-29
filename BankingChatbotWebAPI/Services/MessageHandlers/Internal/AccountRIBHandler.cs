using System;
using BankingChatbotWebAPI.Services.Connector.Bank.Models;
using BankingChatbotWebAPI.Services.MessageHandlers.Internal;
using BankingChatbotWebAPI.Services.MessageHandlers.Internal.Exceptions;

namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal;
    
public class AccountRIBHandler : IStateBasedInternalComingMessageHandler
{
    private readonly ICBSService _bankService;

    public AccountRIBHandler(ICBSService bankService)
    {
        _bankService = bankService;
    }

    public InternalSessionState State => InternalSessionState.ACCOUNT_INQUIRY_RIB_CHOOSE_ACCOUNT_MENU;

    public async Task<(string Message, InternalSessionState NextState)> Handle(string sessionId, string text)
    {
        if (text is null)
        {
            throw new WrongUserInputException(nameof(InternalSessionState.ACCOUNT_INQUIRY_RIB_CHOOSE_ACCOUNT_MENU));
        }

        return await GetAccountRIB(sessionId, text);
    }

    private async Task<(string Message, InternalSessionState NextState)> GetAccountRIB(string sessionId, string account)
    {
        var rib = await _bankService.GetAccountRIB(sessionId, account);

        if (rib is null)
        {
            throw new BankDataNotFoundException($"No rib details found for Account {account} and sessionId {sessionId}", null);
        }

        return (ProcessTemplate(InternalReturnMessage.ACCOUNT_RIB_TEMPLATE, rib), InternalSessionState.GREETING);
    }

    private string ProcessTemplate(string template, AccountRIB rib)
    {
        return template.Replace($"@{nameof(rib.Title)}", rib.Title)
                .Replace($"@{nameof(rib.RIB)}", rib.RIB)
                .Replace($"@{nameof(rib.IBAN)}", rib.IBAN)
                .Replace($"@{nameof(rib.BIC)}", rib.BIC);
    }
}
