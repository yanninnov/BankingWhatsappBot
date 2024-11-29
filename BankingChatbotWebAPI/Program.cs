using BankingChatbotWebAPI.Middleware;
using BankingChatbotWebAPI.Services;
using BankingChatbotWebAPI.Services.Connector.Bank;
using BankingChatbotWebAPI.Services.Gateways;
using BankingChatbotWebAPI.Services.MessageHandlers;
using BankingChatbotWebAPI.Services.MessageHandlers.Internal;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) 
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    // Add services to the container.

    // Register Twilio service as a singleton
    builder.Services.AddSingleton<IWhatsappGateway, TwilioWhatsappGateway>();

    // Register services for conversational engine
    builder.Services.AddSingleton<IStateBasedInternalComingMessageHandler, AccountBalanceHandler>();
    builder.Services.AddSingleton<IStateBasedInternalComingMessageHandler, AccountHistoryHandler>();
    builder.Services.AddSingleton<IStateBasedInternalComingMessageHandler, AccountInquiryHandler>();
    builder.Services.AddSingleton<IStateBasedInternalComingMessageHandler, AccountRIBHandler>();
    builder.Services.AddSingleton<IStateBasedInternalComingMessageHandler, GreetingHandler>();
    builder.Services.AddSingleton<IStateBasedInternalComingMessageHandler, PrincipalMenuHandler>();
    builder.Services.AddSingleton<IIncomingMessageHandler, InternalIncomingMessageHandler>();
    builder.Services.AddSingleton<IConversationalEngine, ConversationalEngine>();

    // Register bank services
    builder.Services.AddSingleton<ICBSService, DummyBankConnector>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Use Serilog as the logging provider
    builder.Host.UseSerilog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseMiddleware<TwilioRestrictionMiddleware>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application failed to start correctly.");
}
finally
{
    Log.CloseAndFlush();
}
