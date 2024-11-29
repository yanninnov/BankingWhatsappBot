using System;
using System.ComponentModel.DataAnnotations;

namespace BankingChatbotWebAPI.Controllers.Models;

public record IncomingMessage(string From, string Body, string MessageSid)
{
}