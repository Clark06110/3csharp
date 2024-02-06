FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["3CSharpProj.Console/3CSharpProj.Console.csproj", "3CSharpProj.Console/"]
RUN dotnet restore "3CSharpProj.Console/3CSharpProj.Console.csproj"
COPY . .
WORKDIR "/src/3CSharpProj.Console"
RUN dotnet build "3CSharpProj.Console.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "3CSharpProj.Console.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "3CSharpProj.Console.dll"]
