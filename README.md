# DreamStore
🛒 Project Description (Web API)
This is a Web API for an online electronics store (laptops, tablets, and smartphones), providing a full set of RESTful endpoints for interaction with a frontend or mobile application.

Key Features:
🔐 User authentication and authorization (JWT tokens)

🧑‍💻 User roles (e.g., admin, customer)

💻📱 Manage products: laptops, tablets, smartphones

🏷 Full CRUD operations for products, brands, categories, and users

🔎 Search products by name

🛠 Admin-level access to manage all entities through the API

![image](https://github.com/user-attachments/assets/3d404754-7354-428c-a126-e1d7a725eb03)


Project Setup Instructions (ASP.NET Core + SQL Server)
⚙️ Requirements
Before you begin, make sure the following are installed:

.NET SDK 9.0+
SQL Server
Visual Studio 2022 or Visual Studio Code (with the C# extension)

🔧 How to Run the Project
1. Clone the Repository
git clone https://github.com/sayron2332/DreamStore and open:
cd DreamStore

Then run the following command to apply migrations and create the database:
dotnet ef database update --project DreamStore.Infrastructure --startup-project DreamStore.Api

and than you need to open Api project:
cd DreamStore.Api
 
and finish run:
dotnet run

🔑 Admin Login Credentials
By default, the project seeds an admin user when it starts:
Email: admin@example.com
Password: Admin123!
These can be changed in the SeedData.cs or DbInitializer.cs file if needed.
