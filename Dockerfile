FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files
COPY QuantityMeasurementModelLayer/*.csproj QuantityMeasurementModelLayer/
COPY QuantityMeasurementRepositoryLayer/*.csproj QuantityMeasurementRepositoryLayer/
COPY QuantityMeasurementBusinessLayer/*.csproj QuantityMeasurementBusinessLayer/
COPY QuantityMeasurementAPILayer/*.csproj QuantityMeasurementAPILayer/

# Restore
RUN dotnet restore QuantityMeasurementAPILayer/QuantityMeasurementAPILayer.csproj

# Copy everything
COPY . .

# Publish
RUN dotnet publish QuantityMeasurementAPILayer/QuantityMeasurementAPILayer.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "QuantityMeasurementAPILayer.dll"]