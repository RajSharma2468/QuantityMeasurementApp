FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy entire solution
COPY . .

# Restore all projects
RUN dotnet restore QuantityMeasurementAPILayer/QuantityMeasurementAPILayer.csproj

# Publish API
RUN dotnet publish QuantityMeasurementAPILayer/QuantityMeasurementAPILayer.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "QuantityMeasurementAPILayer.dll"]