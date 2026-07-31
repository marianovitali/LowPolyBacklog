# 🎮 Low Poly Backlog

> A retro PlayStation-inspired web application to organize, track and manage your personal video game collection and backlog.

<div align="center">

![.NET](https://img.shields.io/badge/.NET%2010-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23%2014-239120?style=for-the-badge&logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core_MVC-5C2D91?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Azure SQL](https://img.shields.io/badge/Azure_SQL-0078D4?style=for-the-badge&logo=microsoftazure&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![Render](https://img.shields.io/badge/Render-46E3B7?style=for-the-badge&logo=render&logoColor=black)
![Cloudinary](https://img.shields.io/badge/Cloudinary-3448C5?style=for-the-badge&logo=cloudinary&logoColor=white)

</div>

---

## 📸 Preview

> *(Screenshots coming soon)*

---

## 🌐 Live Demo

| Service | Link |
|---------|------|
|  [Web Application |](https://lowpolybacklog-web.onrender.com/)
|  [Swagger | ](https://lowpolybacklog-api.onrender.com/swagger/index.html)

---

## Features

### Frontend

- Retro PlayStation-inspired interface
- Dashboard with backlog statistics
- Game library management
- Backlog organization
- Admin mode
- Responsive layout

### Backend

- RESTful API
- Repository Pattern
- Service Layer
- Entity Framework Core
- SQL Server
- Azure SQL Database
- API Key Authentication
- AutoMapper
- Pagination & Filtering
- Cloudinary image uploads
- IGDB integration

---

## Architecture

```
Browser
    │
    ▼
ASP.NET Core MVC
    │
HttpClientFactory
    │
    ▼
ASP.NET Core Web API
    │
Service Layer
    │
Repository Layer
    │
Entity Framework Core
    │
Azure SQL Database
```

External services

```
IGDB API
Cloudinary
Render
Azure SQL
```

---

## Solution Structure

```
LowPolyBacklog/

├── LowPolyBacklogApi/
│
├── LowPolyBacklogWeb/
│
├── LowPolyBacklogShared/
│
└── docker-compose.yml
```

---

## Tech Stack

### Backend

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Azure SQL

### Frontend

- ASP.NET Core MVC
- Razor Views
- Bootstrap

### Cloud

- Azure SQL Database
- Render
- Cloudinary
- IGDB API

### DevOps

- Docker
- Docker Compose

---

## Running Locally

Clone the repository

```bash
git clone https://github.com/marianovitali/LowPolyBacklog.git

cd LowPolyBacklog
```

Configure your User Secrets

```bash
dotnet user-secrets init

# Database
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your_connection_string>"

# Cloudinary
dotnet user-secrets set "CloudinarySettings:CloudName" "<cloud_name>"
dotnet user-secrets set "CloudinarySettings:ApiKey" "<api_key>"
dotnet user-secrets set "CloudinarySettings:ApiSecret" "<api_secret>"

# IGDB
dotnet user-secrets set "IgdbSettings:ClientId" "<client_id>"
dotnet user-secrets set "IgdbSettings:ClientSecret" "<client_secret>"

# Security
dotnet user-secrets set "ApiKey" "<api_key>"
```

Run with Docker

```bash
docker compose up --build
```

or

Run directly

```bash
dotnet ef database update

dotnet run
```

---

## Roadmap

- ✅ ASP.NET Core Web API
- ✅ ASP.NET MVC Frontend
- ✅ Azure SQL Database
- ✅ Docker Support
- ✅ Cloudinary Integration
- ✅ IGDB Integration
- ✅ Render Deployment

Planned

- Authentication
- User Accounts
- Game Collections
- Statistics
- GitHub Actions CI/CD
- Automated Testing

---

## 👨‍💻 Author

**Mariano Vitali**

Backend Developer (.NET)

GitHub:
https://github.com/marianovitali
