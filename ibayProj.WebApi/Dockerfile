FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ibayProj.WebApi/ibayProj.WebApi.csproj", "ibayProj.WebApi/"]
RUN dotnet restore "ibayProj.WebApi/ibayProj.WebApi.csproj"
COPY . .
WORKDIR "/src/ibayProj.WebApi"
RUN dotnet build "ibayProj.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ibayProj.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ibayProj.WebApi.dll"]
