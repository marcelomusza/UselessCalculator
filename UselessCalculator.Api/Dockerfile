FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["UselessCalculator.Api/UselessCalculator.Api.csproj", "UselessCalculator.Api/"]
COPY ["UselessCalculator.Application/UselessCalculator.Application.csproj", "UselessCalculator.Application/"]
RUN dotnet restore "UselessCalculator.Api/UselessCalculator.Api.csproj"

COPY . .

RUN dotnet build "UselessCalculator.Api/UselessCalculator.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "UselessCalculator.Api/UselessCalculator.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

USER app
ENTRYPOINT ["dotnet", "UselessCalculator.Api.dll"]
