# msa3edcom Admin — ASP.NET Core MVC Landing Page CMS

A simple, production-ready ASP.NET Core MVC project that pairs a **premium bilingual (EN/AR) landing page** with a **lightweight admin dashboard** for editing content without touching code.

---

## What's Included

- ✅ Public landing page (Bootstrap 5, light/dark theme, RTL Arabic, animations)
- ✅ Admin dashboard with login (ASP.NET Core Identity)
- ✅ Edit hero / about / general settings / social links from one form
- ✅ Manage client logos (add / delete with image upload)
- ✅ Image upload with size + extension validation, saved to `wwwroot/uploads/`
- ✅ EF Core + SQL Server (LocalDB by default)
- ✅ Auto-migrate + seed default content + admin user on first run

---

## Prerequisites

- **.NET 8 SDK** — [download](https://dotnet.microsoft.com/download)
- **Visual Studio 2022** (17.8+) or **VS Code** with C# extension
- **SQL Server LocalDB** (comes with VS) or full SQL Server

---

## Quick Start

### Option A — Visual Studio

1. Open `Msa3edcomAdmin.sln`
2. Press **F5** (or **Ctrl+F5** to run without debugger)
3. The database is created automatically on first launch

### Option B — Command Line

```bash
cd Msa3edcomAdmin

# Restore packages
dotnet restore

# Run (creates DB automatically and seeds default admin)
dotnet run
```

The app runs at `https://localhost:5001` and `http://localhost:5000`.

---

## Default Admin Credentials

```
URL:      /Account/Login
Email:    admin@msa3edcom.com
Password: Admin@12345
```

> **⚠ Change this password before deploying to production.** Do it via the Identity tables directly or extend the controller with a "change password" action.

---

## Project Structure

```
Msa3edcomAdmin/
├── Controllers/
│   ├── HomeController.cs          # Public landing page + language switching
│   ├── AccountController.cs       # Login / Logout
│   └── AdminController.cs         # All admin actions (settings, clients, uploads)
├── Models/
│   ├── ApplicationDbContext.cs    # EF Core context
│   ├── SiteContent.cs             # Single row holding all editable site text
│   └── ClientItem.cs              # Multiple rows for client logos
├── ViewModels/
│   ├── LandingPageViewModel.cs
│   └── LoginViewModel.cs
├── Views/
│   ├── Home/Index.cshtml          # The landing page
│   ├── Account/Login.cshtml
│   ├── Admin/
│   │   ├── Index.cshtml           # Dashboard home
│   │   ├── Settings.cshtml        # Edit site content
│   │   └── Clients.cshtml         # Manage client logos
│   └── Shared/_AdminLayout.cshtml
├── wwwroot/
│   ├── css/
│   │   ├── site.css               # Public site styles (themes, RTL, animations)
│   │   └── admin.css              # Admin dashboard styles
│   ├── js/
│   │   ├── site.js                # Theme toggle, scroll, reveal
│   │   └── admin.js               # Sidebar toggle, auto-dismiss alerts
│   └── uploads/                   # User-uploaded images (gitignored)
│       ├── logos/
│       ├── hero/
│       ├── about/
│       └── clients/
├── Properties/launchSettings.json
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
└── Msa3edcomAdmin.csproj
```

---

## How It Works

### Public Site

- `/` renders the landing page with content pulled from the `SiteContent` and `ClientItem` tables
- **Default language is Arabic (RTL).** Click the `EN` button in the navbar to switch
- Selected language is persisted in a cookie (`/?lang=en` also works)
- Light / dark theme toggle in navbar (persisted in `localStorage`)

### Admin Dashboard

- `/Admin` redirects to login if not authenticated
- **Site Content** page edits everything in one form:
  - General (logo, phone, WhatsApp, email, footer text, social links)
  - Hero section (titles + subtitles in EN/AR + image + CTA button)
  - About section (titles + texts in EN/AR + image)
- **Clients** page lists all client logos with an inline add form and delete buttons

### File Uploads

- Allowed: `.jpg`, `.jpeg`, `.png`, `.webp`, `.svg`
- Max size: **4 MB** (configurable in `AdminController.cs`)
- Files stored under `wwwroot/uploads/{logos|hero|about|clients}/{guid}.ext`
- Old file is deleted when replaced
- Path traversal is blocked

---

## Database

- Default connection: **LocalDB** → `Server=(localdb)\MSSQLLocalDB;Database=Msa3edcomAdmin;...`
- Tables created automatically on first run (`db.Database.Migrate()`)
- For full SQL Server, edit `appsettings.json` → `ConnectionStrings:DefaultConnection`

### Manual migrations (optional)

If you want to manage migrations explicitly:

```bash
dotnet tool install --global dotnet-ef     # one time
dotnet ef migrations add InitialCreate
dotnet ef database update
```

The included `Program.cs` calls `db.Database.Migrate()` on startup, so even without an existing migration the database will be created from the model on first run.

---

## Customizing the Landing Page

The whole landing page is in **`Views/Home/Index.cshtml`**. Sections that should be editable read from `Model.Content` (a `SiteContent` instance) and `Model.Clients`. Sections that don't need to be editable (services list, contact cards, etc.) are hard-coded in the Razor file — edit them there directly.

To make a hard-coded section editable from admin:
1. Add fields to `SiteContent.cs`
2. Add a migration: `dotnet ef migrations add AddNewFields` then `dotnet ef database update`
3. Add inputs to `Views/Admin/Settings.cshtml`
4. Map them in `AdminController.Settings(POST)`
5. Reference them in `Views/Home/Index.cshtml`

---

## Production Checklist

- [ ] Change `DefaultAdmin:Password` in `appsettings.json` (or use environment variable)
- [ ] Update `ConnectionStrings:DefaultConnection` to a real SQL Server instance
- [ ] Set `ASPNETCORE_ENVIRONMENT=Production`
- [ ] Run behind HTTPS (the project already enables HSTS in non-Development)
- [ ] Back up the database regularly
- [ ] Back up `wwwroot/uploads/` — those files are not in source control

---

## Tech Stack

| Layer | Choice |
|---|---|
| Framework | ASP.NET Core 8 MVC |
| ORM | Entity Framework Core 8 (SQL Server) |
| Auth | ASP.NET Core Identity |
| UI | Bootstrap 5.3 + Bootstrap Icons (CDN) |
| Fonts | Inter (Latin) + IBM Plex Sans Arabic |

---

## License

Proprietary — © msa3edcom

## Contact

- **Phone:** +965 6100 1089
- **Email:** Abanoubhabak98@gmail.com
- **WhatsApp:** wa.me/96561001089
