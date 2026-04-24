FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlogApp.API/BlogApp.API.csproj", "BlogApp.API/"]
COPY ["BlogApp.Postgre/BlogApp.Postgre.csproj", "BlogApp.Postgre/"]
COPY ["BlogApp.Domain/BlogApp.Domain.csproj", "BlogApp.Domain/"]
COPY ["BlogApp.Repositories/BlogApp.Repositories.csproj", "BlogApp.Repositories/"]
COPY ["BlogApp.Services/BlogApp.Services.csproj", "BlogApp.Services/"]
RUN dotnet restore "BlogApp.API/BlogApp.API.csproj"
COPY . .
WORKDIR "/src/BlogApp.API"
RUN dotnet build "BlogApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlogApp.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogApp.API.dll"]
