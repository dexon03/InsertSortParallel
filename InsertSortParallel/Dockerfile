FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InsertSortParallel/InsertSortParallel.csproj", "InsertSortParallel/"]
RUN dotnet restore "InsertSortParallel/InsertSortParallel.csproj"
COPY . .
WORKDIR "/src/InsertSortParallel"
RUN dotnet build "InsertSortParallel.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InsertSortParallel.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InsertSortParallel.dll"]
