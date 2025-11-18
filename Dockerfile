# --- Base image: runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# --- Build image: SDK ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Tüm repo içeriğini kopyala
COPY . .

# Restore
RUN dotnet restore PreSaleForm.API/PreSaleForm.API.csproj

# Publish
RUN dotnet publish PreSaleForm.API/PreSaleForm.API.csproj -c Release -o /app/publish

# --- Final runtime image ---
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PreSaleForm.API.dll"]
