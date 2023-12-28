FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5300

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ["./Identity.WebApi/Identity.WebApi.csproj", "Identity.WebApi/"]
RUN dotnet restore "Identity.WebApi/Identity.WebApi.csproj"
COPY . .
WORKDIR "/src/Identity.WebApi"
RUN dotnet build "Identity.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.WebApi.dll"]
