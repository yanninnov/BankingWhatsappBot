using System;
using System.Net;
using Twilio.Security;

namespace BankingChatbotWebAPI.Middleware;

public class TwilioRestrictionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HashSet<string> _allowedIpRanges = new()
    {
        "54.172.60.0/23",
        "54.174.43.0/24",
        // Add all other IP ranges from Twilio's documentation
    };

    private readonly string _authToken; // Twilio Auth Token for request validation

    public TwilioRestrictionMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _authToken = configuration["Twilio:AuthToken"]
            ?? throw new ArgumentNullException("Twilio:AuthToken", "Twilio Auth Token is not configured.");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Apply checks only if the request targets the Message controller
        if (context.Request.Path.StartsWithSegments("/Message", StringComparison.OrdinalIgnoreCase))
        {
            // Check if the request comes from an allowed IP range
            var ipAddress = context.Connection.RemoteIpAddress;
            if (ipAddress == null || !_allowedIpRanges.Any(range => IsIpInRange(ipAddress, range)))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: IP not allowed");
                return;
            }

            // Validate the Twilio request signature
            if (!ValidateTwilioRequest(context))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid Twilio signature");
                return;
            }
        }

        // Proceed to the next middleware
        await _next(context);
    }

    private bool ValidateTwilioRequest(HttpContext context)
    {
        var twilioSignature = context.Request.Headers["X-Twilio-Signature"].FirstOrDefault();
        if (string.IsNullOrEmpty(twilioSignature))
            return false;

        var requestUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}";
        var formParameters = context.Request.HasFormContentType
            ? context.Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString())
            : new Dictionary<string, string>();

        var validator = new RequestValidator(_authToken);
        return validator.Validate(requestUrl, formParameters, twilioSignature);
    }

    private bool IsIpInRange(IPAddress ipAddress, string range)
    {
        // Implement or use a library to check if the IP address is within the CIDR range
        return ipAddress.ToString().Equals(range);    }
}


