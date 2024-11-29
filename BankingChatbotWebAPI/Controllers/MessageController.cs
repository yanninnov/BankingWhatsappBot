using BankingChatbotWebAPI.Controllers.Models;
using BankingChatbotWebAPI.Services;
using BankingChatbotWebAPI.Services.Gateways;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MessageWebHookController : ControllerBase
{
    private IConversationalEngine _conversationalEngine;
    private IWhatsappGateway _whatsappGateway;

    public MessageWebHookController(IConversationalEngine conversatinalEngine, IWhatsappGateway whatsappGateway)
    {
        _conversationalEngine = conversatinalEngine;
        _whatsappGateway = whatsappGateway;
    }

    // Endpoint to handle incoming WhatsApp messages
    [HttpPost("receive")]
    public async Task<IActionResult> ReceiveWhatsappMessage([FromForm] IncomingMessage message)
    {
        var messageText = message.Body;
        var fromNumber = message.From;

        // Process Message
        var response = await _conversationalEngine.ProcessUserInput(fromNumber, messageText);

        // Return response
        var formatedResponse = _whatsappGateway.FormatResponse(response);
        return Content(formatedResponse, "application/xml");
    }
}
