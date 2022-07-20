#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FidelidadeBE.API/FidelidadeBE.API.csproj", "FidelidadeBE.API/"]
COPY ["src/FidelidadeBE.Infra/FidelidadeBE.Infra.csproj", "FidelidadeBE.Infra/"]
COPY ["src/FidelidadeBE.Application/FidelidadeBE.Application.csproj", "FidelidadeBE.Application/"]
COPY ["src/FidelidadeBE.Data/FidelidadeBE.Data.csproj", "FidelidadeBE.Data/"]
COPY ["src/FidelidadeBE.Business/FidelidadeBE.Business.csproj", "FidelidadeBE.Business/"]
COPY ["src/FidelidadeBE.Core/FidelidadeBE.Core.csproj", "FidelidadeBE.Core/"]
RUN dotnet restore "src/FidelidadeBE.API/FidelidadeBE.API.csproj"
COPY . .
WORKDIR "/src/FidelidadeBE.API"
RUN dotnet build "FidelidadeBE.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FidelidadeBE.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FidelidadeBE.API.dll"]