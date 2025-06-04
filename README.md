# DreamStore

Project Setup Instructions (ASP.NET Core + SQL Server)
⚙️ Requirements
Before you begin, make sure the following are installed:

.NET SDK 9.0+
SQL Server
Visual Studio 2022 or Visual Studio Code (with the C# extension)

🔧 How to Run the Project
1. Clone the Repository
git clone https://github.com/sayron2332/DreamStore
cd DreamStore

add connString to appseting json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MyAppDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

Make sure you have the Entity Framework CLI installed:
dotnet tool install --global dotnet-ef

Then run the following command to apply migrations and create the database:
dotnet ef database update

Run the Project
You can now run the project using:

dotnet run
Or press F5 in Visual Studio.

🔑 Admin Login Credentials
By default, the project seeds an admin user when it starts:
Email: admin@example.com
Password: Admin123!
These can be changed in the SeedData.cs or DbInitializer.cs file if needed.
