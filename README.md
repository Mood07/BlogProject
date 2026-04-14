# 📝 Blog Platform — ASP.NET Core MVC

A full-featured **blog platform** built with **ASP.NET Core MVC**, featuring an admin panel, Stripe payment integration, email subscriptions, and a comment system.

---

## 🚀 Project Overview

A production-ready blog application demonstrating full-stack .NET development with clean MVC architecture, third-party integrations, and database management via Entity Framework Core.

---

## 🧠 Key Features

- 📋 **Admin Panel** — create, edit, delete blog posts
- 💳 **Stripe Integration** — donation/payment system
- 📧 **Email Subscription** — newsletter subscriber management
- 💬 **Comment System** — reader interaction on posts
- 🗄️ **Entity Framework Core** — ORM with SQLite/SQL Server
- 🎨 **Responsive MVC Views** — clean Razor frontend

---

## 📁 Project Structure

```
BlogProject/
├── Controllers/
│   ├── AdminController.cs       # Admin panel logic
│   ├── BlogController.cs        # Blog post CRUD
│   ├── DonateController.cs      # Stripe donation
│   ├── HomeController.cs        # Home page
│   └── SubscribeController.cs   # Email subscription
├── Models/
│   ├── BlogPost.cs
│   ├── Donation.cs
│   └── Subscriber.cs
├── Views/                       # Razor view templates
├── Data/                        # EF Core DbContext
├── Migrations/                  # EF Core migrations
├── wwwroot/                     # Static files (CSS, JS)
├── Program.cs
└── appsettings.json
```

---

## 🧰 Tech Stack

| Category | Tools |
|----------|-------|
| Language | C# / .NET 8 |
| Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core |
| Database | SQLite |
| Payments | Stripe API |
| Frontend | Razor Views, Bootstrap |

---

## ⚙️ Setup & Run

### 1) Install Dependencies

```bash
dotnet restore
```

### 2) Apply Migrations

```bash
dotnet ef database update
```

### 3) Configure Stripe (appsettings.json)

```json
"Stripe": {
  "SecretKey": "your_secret_key",
  "PublishableKey": "your_publishable_key"
}
```

### 4) Run the Application

```bash
dotnet run
```

App will be available at `http://localhost:5000`

---

## 👨‍💻 Author

**Berke Arda Türk**  
Data Science & AI Enthusiast | Computer Science (B.ASc)  
[🌐 Portfolio Website](https://berkeardaturk.com) • [💼 LinkedIn](https://www.linkedin.com/in/berke-arda-turk/) • [🐙 GitHub](https://github.com/Mood07)
