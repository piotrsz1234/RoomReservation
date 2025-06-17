FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RoomReservation.Application/RoomReservation.Application.csproj", "RoomReservation.Application/"]
COPY ["RoomReservation.Implementation/RoomReservation.Implementation.csproj", "RoomReservation.Implementation/"]
COPY ["RoomReservation.Domain/RoomReservation.Domain.csproj", "RoomReservation.Domain/"]
RUN dotnet restore "RoomReservation.Application/RoomReservation.Application.csproj"
COPY . .
WORKDIR "/src/RoomReservation.Application"
RUN dotnet build "./RoomReservation.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./RoomReservation.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN mkdir dataprotection-keys

COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT="Development"

ENTRYPOINT ["dotnet", "RoomReservation.Application.dll"]
