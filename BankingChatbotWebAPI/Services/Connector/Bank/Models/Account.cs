using BankingChatbotWebAPI.Services.Connector.Bank.Exceptions;

namespace BankingChatbotWebAPI.Services.Connector.Bank.Models;

public class Account
{
    public string Name { get; init; }
    public string Number { get; init; }

    public Account(string name, string number)
    {
        if (name is null || number is null)
        {
            throw new BankModelException($"{nameof(Account)} model arguments {nameof(name)} or {nameof(number)} are null");
        }

        this.Name = name;
        this.Number = number;
    }
    
    public override string ToString()
    {
        return $"{Name} {Number}";
    }
}
