FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7086
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ["./Typecode.Domain/Typecode.Domain.csproj", "Typecode.Domain/"]
COPY ["./Typecode.Persistence/Typecode.Persistence.csproj", "Typecode.Persistence/"]
COPY ["./Typecode.Application/Typecode.Application.csproj", "Typecode.Application/"]
COPY ["./Typecode.WebApi/Typecode.WebApi.csproj", "Typecode.WebApi/"]
RUN dotnet restore "Typecode.WebApi/Typecode.WebApi.csproj"
COPY . .
WORKDIR "/src/Typecode.WebApi"
RUN dotnet build "Typecode.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Typecode.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Typecode.WebApi.dll"]
