using System;
namespace BankingChatbotWebAPI.Services.Connector.Bank.Exceptions
{
    public class BankModelException : Exception
    {
        public BankModelException(string? message) : base(message)
        {
        }
    }
}

