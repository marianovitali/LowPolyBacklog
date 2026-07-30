
# ---------- Build ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["LowPolyBacklogApi/LowPolyBacklogApi.csproj", "LowPolyBacklogApi/"]

RUN dotnet restore "LowPolyBacklogApi/LowPolyBacklogApi.csproj"

COPY . .

WORKDIR "/src/LowPolyBacklogApi"

RUN dotnet publish -c Release -o /app/publish

# ---------- Runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "LowPolyBacklogApi.dll"]