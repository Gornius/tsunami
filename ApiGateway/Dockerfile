FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build

WORKDIR /app

COPY Program.cs ./Program.cs
COPY Startup.cs ./Startup.cs
COPY ApiGateway.csproj ./ApiGateway.csproj
COPY appsettings.Development.json ./appsettings.Development.json
COPY appsettings.json ./appsettings.json
COPY configuration.json ./configuration.json
COPY Properties ./Properties

RUN dotnet build

EXPOSE 7779
ENTRYPOINT ["sh", "bin/Debug/net6.0/ApiGateway"]