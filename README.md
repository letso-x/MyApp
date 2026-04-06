Task Manager (ASP.NET Core MVC) -- Live Deployment

A full-stack task management web application built with ASP.NET Core MVC, featuring authentication, per-user data isolation, and real-time task status updates.

Live App: https://tseqetatm-gxc7c2gub0defnbh.southafricanorth-01.azurewebsites.net/

Core Functionality:
- User authentication (login/logout)
- Create tasks
- Edit tasks
- Delete tasks
- Update task status (Pending, In Progress, Completed)
- User-specific data filtering (each user only sees their own tasks)

Tech Stack:
- Backend

- ASP.NET Core MVC
- Entity Framework Core
- SQL Database (Azure SQL / local SQL Server)

-Frontend

- Razor Views
- Bootstrap + custom CSS (glassmorphism UI)

-Deployment

- Microsoft Azure App Service


Project Structure:

- /Controllers
    TaskItemsController.cs
    AuthController.cs

- /Models
    TaskItem.cs

- /Data
    MyAppContext.cs

- /Views
    /TaskItems
        Index.cshtml
        Create.cshtml
        Edit.cshtml
        Delete.cshtml
    - /Auth
        Login.cshtml

- /Views/Shared
    _Layout.cshtml

- /wwwroot
    /css
    /js

- Data Model:
TaskItem
* Id (int)
* Name (string)
* Description (string)
* Status (string)
* UserId (int)
* User (User)

User
* Id (int)
* Email (string)
* PasswordHash (string)
* Tasks (TaskItem)

###Key Features Explained
1. User Isolation

All queries filter by authenticated user:
- ```var userId = GetUserId();_context.TaskItems.Where(x => x.UserId == userId);```
- Prevents users from accessing each other’s data.

2. Status Updates (Inline)
- ```<select name="status" onchange="this.form.submit()">```
- Handled by a POST action that updates and saves immediately.

3. Secure Deletion
Delete operations
- Require POST
- Validate ownership
- Remove from database
- Redirect to index

4. Authentication
   ```User.FindFirst(ClaimTypes.NameIdentifier)```
   
### Setup Instructions
1. Clone the repository
git clone https://github.com/letso-x/TaskManager

2. Configure database
Update connection string in: ```appsettings.json```

3. Run migrations
- ```dotnet ef database update```

4. Run the application
- ```dotnet run```

### Known Limitations
- No client-side validation
- Status stored as string (should be enum)
- No pagination or search
- No role-based authorization

### Potential Improvements
- Convert Status to enum
- Add validation (FluentValidation or DataAnnotations)
- Introduce DTOs/ViewModels
- Add API layer (REST endpoints)
- Implement unit tests
- Improve accessibility and UX

### Purpose
This project is designed as:
- A portfolio-ready ASP.NET Core MVC application
- A demonstration of CRUD operations with authentication
- A foundation for expanding into a more complex system
