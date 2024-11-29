namespace BankingChatbotWebAPI.Services;

public interface IWhatsappGateway
{
    string FormatResponse(string message);
}