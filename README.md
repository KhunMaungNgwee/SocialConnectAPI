SocialConnectAPI 🌐

A modern social media backend API built with ASP.NET Core, featuring user authentication, posts, comments, reactions, and real-time interactions.

🚀 Features
🔐 User Authentication - JWT-based authentication with secure password hashing

📝 Posts Management - Create, edit, delete, and view posts with images

💬 Comments System - Comment on posts with nested replies support

❤️ Reactions - Like posts and comments

👤 User Profiles - View user profiles with statistics and activity feed

🔒 Role-based Authorization - Secure endpoints with proper authorization

📊 Pagination - Efficient data loading with pagination support

📄 Swagger Documentation - Interactive API documentation

🔄 Real-time Updates - WebSocket support for live notifications

🛠️ Tech Stack
Backend: ASP.NET Core 10.0

Authentication: JWT (JSON Web Tokens)

Database: SQL Server with Entity Framework Core

Documentation: Swagger/OpenAPI

Testing: xUnit / Moq

Containerization: Docker

📦 Installation & Setup
Prerequisites
.NET 10.0 SDK

SQL Server

Visual Studio 2022 or VS Code

Step 1: Clone the Repository
bash
git clone https://github.com/KhunMaungNgwe/SocialConnectAPI.git
cd SocialConnectAPI
Step 2: Configure Database
Update the connection string in appsettings.json:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SocialAppDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
Step 3: Run Migrations
bash
dotnet ef database update
Step 4: Run the Application
bash
dotnet run
The API will be available at https://localhost:5001 or http://localhost:5000
