#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WS_Server/WS_Server.csproj", "WS_Server/"]
RUN dotnet restore "WS_Server/WS_Server.csproj"
COPY . .
WORKDIR "/src/WS_Server"
RUN dotnet build "WS_Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WS_Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WS_Server.dll"]