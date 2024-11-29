
using BankingChatbotWebAPI.Services;
using Google.Cloud.Dialogflow.V2;
using Parameters = Google.Protobuf.WellKnownTypes.Struct;

namespace BankingChatbotWebAPI.Actions;

public class AccountListProcessingAction : BaseIntentProcessingAction
{
    private readonly ICBSService _cbsService;

    private const string RESPONSE_TEMPLATE = @"Veuillez choisir parmi les comptes suivant :
        @accounts";

    private const string DEFAULT_RESPONSE = @"Aucun compte ne correspond à votre demande.";

    public AccountListProcessingAction(ICBSService cBSService)
    {
        this._cbsService = cBSService;
    }

    public override async Task<WebhookResponse> ExecuteAsync(string session, Parameters parameters)
    {
        return await ExecuteAsync(session);
    }

    public async Task<WebhookResponse> ExecuteAsync(string fromNumber)
    {
        var accounts = await _cbsService.ListAccounts(fromNumber);

        var response = DEFAULT_RESPONSE;

        if(accounts.Any()){
            var accountsAsString = string.Join("", accounts.Select(
                (account, index) => $"{index}. {account}\n\n"
            ));

            response = RESPONSE_TEMPLATE.Replace("@accounts", accountsAsString);
        }

        return new WebhookResponse(){
            FulfillmentText = response
        };
        //return response
    }
}