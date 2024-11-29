using BankingChatbotWebAPI.Services.Connector.Bank.Exceptions;

namespace BankingChatbotWebAPI.Services.Connector.Bank.Models;

public class AccountRIB
{
    public string Title { get; init; }
    public string RIB { get; init; }
    public string? IBAN { get; init; }
    public string? BIC { get; init; }

    public AccountRIB(string title, string rib, string? iban, string? bic)
    {
        if(title is null || rib is null || iban is null || bic is null)
        {
            throw new BankModelException($"{nameof(AccountRIB)} Model arguments {nameof(title)} or {nameof(rib)} are null");
        }

        this.Title = title;
        this.RIB = rib;
        this.IBAN = iban;
        this.BIC = bic;
    }
}
