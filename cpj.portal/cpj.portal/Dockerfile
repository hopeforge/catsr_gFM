#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["cpj.portal/cpj.portal.csproj", "cpj.portal/"]
RUN dotnet restore "cpj.portal/cpj.portal.csproj"
COPY . .
WORKDIR "/src/cpj.portal"
RUN dotnet build "cpj.portal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cpj.portal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cpj.portal.dll"]