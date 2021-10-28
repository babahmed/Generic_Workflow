#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["PublicWorkflow.Api/PublicWorkflow.Api.csproj", "PublicWorkflow.Api/"]
COPY ["PublicWorkflow.Application/PublicWorkflow.Application.csproj", "PublicWorkflow.Application/"]
COPY ["PublicWorkflow.Domain/PublicWorkflow.Domain.csproj", "PublicWorkflow.Domain/"]
COPY ["PublicWorkflow.Infrastructure.Shared/PublicWorkflow.Infrastructure.Shared.csproj", "PublicWorkflow.Infrastructure.Shared/"]
COPY ["PublicWorkflow.Infrastructure/PublicWorkflow.Infrastructure.csproj", "PublicWorkflow.Infrastructure/"]
RUN dotnet restore "PublicWorkflow.Api/PublicWorkflow.Api.csproj"
COPY . .
WORKDIR "/src/PublicWorkflow.Api"
RUN dotnet build "PublicWorkflow.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PublicWorkflow.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PublicWorkflow.Api.dll"]