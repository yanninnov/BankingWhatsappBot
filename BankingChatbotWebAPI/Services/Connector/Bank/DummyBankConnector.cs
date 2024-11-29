using System;
using AutoFixture;
using System.Collections.Generic;
using BankingChatbotWebAPI.Services.Connector.Bank.Models;

namespace BankingChatbotWebAPI.Services.Connector.Bank;

public class DummyBankConnector : ICBSService
{
	public DummyBankConnector()
	{
	}

    public async Task<AccountBalance> GetAccountBalance(string fromNumber, string accountNumber)
    {
        var fixture = new Fixture();
        var accountBalance = fixture.Create<AccountBalance>();

        return accountBalance;
    }

    public async Task<AccountRIB> GetAccountRIB(string fromNumber, string accountNumber)
    {
        var fixture = new Fixture();
        var accountBalance = fixture.Create<AccountRIB>();

        return accountBalance;
    }

    public async Task<AccountManager> GetAccountManager(string fromNumber)
    {
        var fixture = new Fixture();
        var accountManager = fixture.Create<AccountManager>();

        return accountManager;
    }

    public Task<UnpaidSummary> GetUnpaidBalance(string fromNumber, string accountNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Account>> ListAccounts(string fromNumber)
    {
        var fixture = new Fixture();
        var accounts = fixture.CreateMany<Account>(2);

        return accounts.ToList();
    }

    public async Task<IEnumerable<AccountHistoryLine>> GetAccountHistory(string fromNumber, string accountNumber)
    {
        var fixture = new Fixture();
        var history = fixture.CreateMany<AccountHistoryLine>(2);

        return history.ToList();
    }
}

