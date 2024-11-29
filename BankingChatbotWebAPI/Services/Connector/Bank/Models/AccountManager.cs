using BankingChatbotWebAPI.Services.Connector.Bank.Exceptions;

namespace BankingChatbotWebAPI.Services.Connector.Bank.Models;

public class AccountManager
{
    public string Fullname { get; init; }
    public string? Contact { get; init; }
    public string? Address { get; init; }
    public string? Whatsapp { get; init; }

    public AccountManager(string fullname, string contact, string address, string whatsapp)
    {
        if(Fullname is null)
        {
            throw new BankModelException($"{nameof(AccountManager)} model arguments {nameof(fullname)} is null");
        }

        this.Fullname = fullname;
        this.Contact = contact;
        this.Address = address;
        this.Whatsapp = whatsapp;
    }
}
