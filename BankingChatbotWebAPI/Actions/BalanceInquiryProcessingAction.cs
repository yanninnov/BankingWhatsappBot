
using BankingChatbotWebAPI.Services;
using Google.Cloud.Dialogflow.V2;
using Parameters = Google.Protobuf.WellKnownTypes.Struct;

namespace BankingChatbotWebAPI.Actions;

public class BalanceInquiryProcessingAction : BaseIntentProcessingAction
{
    private readonly ICBSService _cbsService;

    private const string RESPONSE_TEMPLATE = @"Votre solde est de @balance. @unpaid.";
    private const string UNPAID_RESPONSE_TEMPLATE = @"Vous avez @amount d'impayés depuis @date";
    private const string MISSING_ACCOUNT_RESPONSE_TEMPLATE = @"Veuillez fournir un numéro de compte correct.";

    private const string DEFAULT_RESPONSE = @"Aucun compte ne correspond à votre demande.";

    public BalanceInquiryProcessingAction(ICBSService cBSService)
    {
        this._cbsService = cBSService;
    }

    public override async Task<WebhookResponse> ExecuteAsync(string session, Parameters parameters)
    {
        var accountNumber = GetParameter(parameters, "account_number");
        
        if(string.IsNullOrEmpty(accountNumber)){
            return new WebhookResponse(){
                FulfillmentText = MISSING_ACCOUNT_RESPONSE_TEMPLATE
            };
        }

        return await ExecuteAsync(session, accountNumber);
    }

    public async Task<WebhookResponse> ExecuteAsync(string fromNumber, string accountNumber)
    {
        var balance = await _cbsService.GetAccountBalance(fromNumber, accountNumber);
        var unpaid = await _cbsService.GetUnpaidBalance(fromNumber, accountNumber);

        var response = DEFAULT_RESPONSE;
        if(balance is not null){
            response = RESPONSE_TEMPLATE.Replace("@balance", $"{balance}");
            response = unpaid is null 
                ? response.Replace("@unpaid", "") 
                : response.Replace("@unpaid", 
                                    UNPAID_RESPONSE_TEMPLATE.Replace("@amount", $"{unpaid.Amount} {unpaid.Currency}")
                                        .Replace("@date", unpaid.StartDate));
        }

        return new WebhookResponse(){
            FulfillmentText = response
        };
        //return response
    }
}