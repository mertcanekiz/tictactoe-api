FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/TicTacToe.API/TicTacToe.API.csproj", "TicTacToe.API/"]
RUN dotnet restore "src/TicTacToe.API/TicTacToe.API.csproj"
COPY . .
WORKDIR "/src/TicTacToe.API"
RUN dotnet build "TicTacToe.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicTacToe.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.API.dll"]
