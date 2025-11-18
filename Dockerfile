# --- Base image: runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# --- Build image: SDK ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution + project files
COPY PreSaleForm.sln ./
COPY PreSaleForm.API/PreSaleForm.API.csproj PreSaleForm.API/
COPY PreSaleForm.Application/PreSaleForm.Application.csproj PreSaleForm.Application/
COPY PreSaleForm.Domain/PreSaleForm.Domain.csproj PreSaleForm.Domain/
COPY PreSaleForm.Infrastructure/PreSaleForm.Infrastructure.csproj PreSaleForm.Infrastructure/

RUN dotnet restore "PreSaleForm.API/PreSaleForm.API.csproj"

# Copy full source
COPY . .

# Publish
WORKDIR /src/PreSaleForm.API
RUN dotnet publish PreSaleForm.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- Final runtime image ---
FROM base AS final
WORKDIR /app

# Copy published artifacts
COPY --from=build /app/publish .

# Copy logo files from build stage first
COPY --from=build /src/PreSaleForm.API/wwwroot/logo.png /app/wwwroot/logo.png
COPY --from=build /src/PreSaleForm.API/wwwroot/logo2.png /app/wwwroot/logo2.png

# Create pdf directory structure with proper permissions
RUN mkdir -p /app/wwwroot/pdf/presale && \
    chmod -R 755 /app/wwwroot

# Environment vars
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Persist wwwroot (Coolify)
VOLUME ["/app/wwwroot"]

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/api/presaleforms/ping || exit 1

ENTRYPOINT ["dotnet", "PreSaleForm.API.dll"]
