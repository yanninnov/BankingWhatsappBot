using BankingChatbotWebAPI.Services.Connector.Bank.Exceptions;

namespace BankingChatbotWebAPI.Services.Connector.Bank.Models;

public class AccountBalance
{
    public string Amount { get; init; }
    public string Currency { get; init; }
    public string From { get; init; }

    public AccountBalance(string amount, string currency, string from)
    {
        if(amount is null || currency is null)
        {
            throw new BankModelException($"{nameof(AccountBalance)}  Balance model arguments {nameof(amount)} or {nameof(currency)} are null");
        }

        this.Amount = amount;
        this.Currency = currency;
        this.From = from;
    }
}
