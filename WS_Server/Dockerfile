FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
EXPOSE 80
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 80

WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "WS_Server.dll"]