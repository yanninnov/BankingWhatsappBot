using Google.Cloud.Dialogflow.V2;
using Microsoft.AspNetCore.Mvc;

namespace BankingChatbotWebAPI.Controllers;

public class FullfilmentController : ControllerBase
{
    private readonly IDictionary<string, BankingChatbotWebAPI.Models.Intent> _intentList;
    private readonly ILogger<FullfilmentController> _logger;

    private static readonly WebhookResponse DEFAULT_RESPONSE = new WebhookResponse
    {
        FulfillmentText = "",
    };

    public FullfilmentController(IIntentFactory factory, ILogger<FullfilmentController> logger)
    {
        _intentList = factory.GetIntents();
        this._logger = logger;
    }


    /*[HttpPost("fulfillment")]
    public async Task<IActionResult> GetWebhookResponse([FromBody] WebhookRequest dialogflowRequest)
    {
        var response = DEFAULT_RESPONSE;

        var intentName = dialogflowRequest.QueryResult.Intent.DisplayName;
        var actualQuestion = dialogflowRequest.QueryResult.QueryText;
        
        this._logger.LogInformation($"Dialogflow Request for intent '{intentName}' and question '{actualQuestion}'");
        
        _intentList.TryGetValue(intentName, out var intent);

        if(intent is null)
        {
            this._logger.LogInformation($"No Processing Matching for intent '{intentName}");
        }else
        {
            if(intent.RequiresPostProcessing)
            {
                var action = intent.ProcessingAction;

                var parameters = dialogflowRequest.QueryResult.Parameters;
                var session = dialogflowRequest.Session;

                response = await action.ExecuteAsync(session, parameters);
            }
        }

        var jsonResponse = response.ToString();
        return new ContentResult { Content = jsonResponse, ContentType = "application/json" }; ;
    }*/ 
}

