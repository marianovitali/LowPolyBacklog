# ---------- Build ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["LowPolyBacklogWeb/LowPolyBacklogWeb.csproj", "LowPolyBacklogWeb/"]

RUN dotnet restore "LowPolyBacklogWeb/LowPolyBacklogWeb.csproj"

COPY . .

WORKDIR "/src/LowPolyBacklogWeb"

RUN dotnet publish -c Release -o /app/publish

# ---------- Runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "LowPolyBacklogWeb.dll"]