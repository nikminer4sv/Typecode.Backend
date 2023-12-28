FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5300

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ["./Notification.WebApi/Notification.WebApi.csproj", "Notification.WebApi/"]
RUN dotnet restore "Notification.WebApi/Notification.WebApi.csproj"
COPY . .
WORKDIR "/src/Notification.WebApi"
RUN dotnet build "Notification.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notification.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.WebApi.dll"]
