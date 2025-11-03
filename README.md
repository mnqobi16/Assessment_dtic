# Assessment_dtic

This is an **ASP.NET Core MVC** application for managing appointments.

---

## Prerequisites
- Visual Studio 2022 or later  
- .NET 6.0 SDK or later  
- SQL Server (local or remote)

---

##  Setup Instructions

1. **Clone the Repository**  
   ```bash
   git clone <your-repo-url>
   ```

2. **Open the Solution**  
   - Launch **Visual Studio**.  
   - Open the `.sln` file in the project root.

3. **Configure the Database**  
   - Update the connection string in `appsettings.json` to point to your SQL Server instance.

4. **Apply Migrations**  
   - Open the **Package Manager Console** in Visual Studio.  
   - Run:
     ```powershell
     Update-Database
     ```
   - This will create the required tables.

5. **Run the Application**  
   - Press **F5** or click the **Start** button in Visual Studio.  
   - The app will launch in your browser.

---

## Features
- Create, edit, and manage appointments
- Create, edit, and manage Patients
- Validate if Date or time already booked
- Validation with **SweetAlert** popups  
- **Entity Framework Core** for data access

---

##  Troubleshooting
- Ensure SQL Server is running and accessible.  
- If migrations fail, check your connection string and permissions.

---
**README file to help set up your ASP.NET Core MVC project in Visual Studio**
