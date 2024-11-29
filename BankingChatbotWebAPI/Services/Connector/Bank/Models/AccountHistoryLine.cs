using BankingChatbotWebAPI.Services.Connector.Bank.Exceptions;

namespace BankingChatbotWebAPI.Services.Connector.Bank.Models;

public class AccountHistoryLine
{
    public string Amount { get; init; }
    public string Type { get; init; }
    public string Date { get; init; }
    public string Description { get; init; }
    public string Currency { get; init; }

    public AccountHistoryLine(string amount, string type, string date, string description, string currency)
    {
        if (amount is null || type is null || description is null || date is null)
        {
            throw new BankModelException($"{nameof(AccountHistoryLine)} model arguments {nameof(amount)} or {nameof(type)} or {nameof(date)} or {nameof(description)} or {nameof(currency)} are null");
        }

        this.Amount = amount;
        this.Type = type;
        this.Date = date;
        this.Description = description;
        this.Currency = currency;
    }
}
