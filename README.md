SocialApp Backend

Technology Stack: .NET Core 10 SDK (C#)

Description:
This is the backend API for the SocialApp project, providing endpoints for user authentication, posts, comments, reactions, profile, and image uploads. It connects to a SQL Server database (SocialAppDB) and serves data to the React frontend.

Features:

RESTful API design

JWT-based authentication (Bearer tokens)

CRUD operations for posts, comments, users, and reactions

Profile and image upload endpoints

Swagger UI enabled for interactive API documentation

Can be run locally using IIS Express

Important Thing ==> please run with IIS Express as the HTTP and HTTPS ports differ.I useed IIS port on my reat app so.

Setup Instructions:

Clone the Repository

git clone https://github.com/KhunMaungNgwee/SocialConnectAPI.git
cd SocialConnectAPI


Restore NuGet Packages

dotnet restore


Database Setup

Database file: SocialAppDB.mdf

Option 1: Attach the .mdf file to SQL Server

Option 2: Run the SQL schema script (SocialAppDB.sql) to create the database

Run the Backend

Open the solution in Visual Studio

Select IIS Express as the launch target

Make note of the HTTP and HTTPS ports used by IIS Express

Start the project to run the API locally

Configure Frontend Connection

Update the API base URL in the frontend .env file to match IIS Express ports

Swagger UI:

After running the project, access Swagger UI at:
https://localhost:{PORT}/swagger (replace {PORT} with your HTTPS port)

Swagger provides interactive documentation for all endpoints, including:

Authentication (/api/Auth)

POST /register – Register a new user

POST /login – Authenticate a user and receive JWT

POST /logout – Logout the user

Posts (/api/posts)

GET /posts – List all posts (supports pagination)

POST /posts – Create a new post

PUT /posts/{postId} – Update a post by ID

POST /posts/{postId}/comments – Add a comment to a post

POST /posts/{postId}/reaction – React to a post

GET /my-posts – Get posts created by the authenticated user

Profile (/api/profile)

GET /profile – Get current user profile information

Image Upload (/api/Upload/image)

POST – Upload an image

DELETE – Delete an image by URL

Notes:

Ensure the frontend is running after the backend for API calls to work properly.

Adjust IIS Express ports if there are conflicts with other local services.

Swagger UI allows testing all endpoints and viewing required request schemas.

Project Repository:
https://github.com/KhunMaungNgwee/SocialConnectAPI.git
