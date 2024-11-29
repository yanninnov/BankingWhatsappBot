using BankingChatbotWebAPI.Services.Connector.Bank.Models;

namespace BankingChatbotWebAPI.Services;

public interface ICBSService
{
    Task<AccountManager> GetAccountManager(string fromNumber);
    Task<AccountRIB> GetAccountRIB(string fromNumber, string accountNumber);
    Task<AccountBalance> GetAccountBalance(string fromNumber, string accountNumber);
    Task<IEnumerable<AccountHistoryLine>> GetAccountHistory(string fromNumber, string accountNumber);

    Task<UnpaidSummary> GetUnpaidBalance(string fromNumber, string accountNumber);
    Task<List<Account>> ListAccounts(string fromNumber);
}
