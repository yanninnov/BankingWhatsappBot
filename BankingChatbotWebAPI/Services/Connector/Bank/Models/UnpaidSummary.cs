namespace BankingChatbotWebAPI.Services.Connector.Bank.Models;

public class UnpaidSummary
{
    public string Amount { get; init; }
    public string Currency { get; init; }
    public string StartDate { get; init; }

    public UnpaidSummary(string amount, string currency, string startDate)
    {
        this.Amount = amount;
        this.Currency = currency;
        this.StartDate = startDate;
    }
}
