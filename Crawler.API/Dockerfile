#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Crawler.API/Crawler.API.csproj", "Crawler.API/"]
COPY ["Crawler.Application/Crawler.Application.csproj", "Crawler.Application/"]
COPY ["Crawler.Domain/Crawler.Domain.csproj", "Crawler.Domain/"]
COPY ["Crawler.Infra.Components.Interfaces/Crawler.Infra.Components.Interfaces.csproj", "Crawler.Infra.Components.Interfaces/"]
COPY ["Crawler.Infra.Components/Crawler.Infra.Components.csproj", "Crawler.Infra.Components/"]
COPY ["Crawler.Infra.Elasticsearch/Crawler.Infra.Elasticsearch.csproj", "Crawler.Infra.Elasticsearch/"]
COPY ["Crawler.Infra.RabbitMq/Crawler.Infra.RabbitMq.csproj", "Crawler.Infra.RabbitMq/"]
COPY ["Crawler.Infra.Redis/Crawler.Infra.Redis.csproj", "Crawler.Infra.Redis/"]
COPY ["Crawler.Web/Crawler.Web.csproj", "Crawler.Web/"]
RUN dotnet restore "Crawler.API/Crawler.API.csproj"
COPY . .
WORKDIR "/src/Crawler.API"
RUN dotnet build "Crawler.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Crawler.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM selenium/standalone-chrome:latest
# Baixar e instalar o ChromeDriver
RUN wget -qO- https://chromedriver.storage.googleapis.com/LATEST_RELEASE | xargs -I{} wget -qO /usr/local/bin/chromedriver "https://chromedriver.storage.googleapis.com/{}/chromedriver_linux64.zip" && \
    unzip /usr/local/bin/chromedriver -d /usr/local/bin && \
    chmod +x /usr/local/bin/chromedrivers

EXPOSE 9515  # porta do ChromeDriver

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crawler.API.dll"]