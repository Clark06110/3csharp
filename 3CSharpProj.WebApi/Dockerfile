FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["3CSharpProj.WebApi/3CSharpProj.WebApi.csproj", "3CSharpProj.WebApi/"]
RUN dotnet restore "3CSharpProj.WebApi/3CSharpProj.WebApi.csproj"
COPY . .
WORKDIR "/src/3CSharpProj.WebApi"
RUN dotnet build "3CSharpProj.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "3CSharpProj.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "3CSharpProj.WebApi.dll"]
