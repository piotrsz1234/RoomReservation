FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["RoomReservation.Api/RoomReservation.Api.csproj", "RoomReservation.Api/"]
COPY ["RoomReservation.Domain/RoomReservation.Domain.csproj", "RoomReservation.Domain/"]
COPY ["RoomReservation.Implementation/RoomReservation.Implementation.csproj", "RoomReservation.Implementation/"]
RUN dotnet restore "RoomReservation.Api/RoomReservation.Api.csproj"
COPY . .
WORKDIR "/src/RoomReservation.Api"
RUN dotnet build "./RoomReservation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RoomReservation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
RUN apt-get update && apt-get install -y \
    libkrb5-3 \
    libgssapi-krb5-2 
USER app
ENV ASPNETCORE_ENVIRONMENT="Development"
ENTRYPOINT ["dotnet", "RoomReservation.Api.dll"]
