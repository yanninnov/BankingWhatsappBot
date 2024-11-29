# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY BankingChatBotWebAPI/*.csproj ./
RUN dotnet restore

# Copy the remaining files and build the application
COPY . .
WORKDIR /src/BankingChatBotWebAPI
RUN dotnet publish -c Release -o /app/out

# Use the official ASP.NET Core Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory in the runtime image
WORKDIR /app

# Copy the build output to the runtime image
COPY --from=build-env /app/out .

# Expose port 80 to the outside world
EXPOSE 81

# Define the entry point for the container
ENV ASPNETCORE_URLS=http://+:81
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENTRYPOINT ["dotnet", "BankingChatbotWebAPI.dll"]

