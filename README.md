# Audit-System
Event-Driven Audit System using Clean Architecture and CQRS

# 📌 Audit System - Event Driven with .NET Core

## 🧠 Overview

This project is a simple **Event-Driven Audit Logging System** built using **ASP.NET Core Web API** following:

* Clean Architecture
* CQRS Pattern (Commands & Queries)
* Background Processing using `BackgroundService`
* Asynchronous Audit Logging

The system tracks user actions (e.g., enrolling in a course) and logs them in the background without affecting API performance.

---

## 🏗️ Project Structure

The project follows **Clean Architecture** principles:

```
AuditSystem
│
├── AuditSystem.Domain
│   ├── Entities
│   └── Common
│
├── AuditSystem.Application
│   ├── Features
│   │   ├── Enrollments
│   │   │   ├── Commands
│   │   │   └── Queries
│   ├── Interfaces
│   └── Events
│
├── AuditSystem.Infrastructure
│   ├── Persistence
│   └── Services (Audit)
│
├── AuditSystem.API
│   ├── Controllers
│   └── Program.cs
```

---

## ⚙️ How the Audit System Works

### 1️⃣ Command Execution

* A user sends a request to enroll in a course via API.
* `EnrollCourseHandler`:

  * Saves the enrollment in the database.
  * Publishes an `AuditEvent` to an in-memory queue.

---

### 2️⃣ Event Queue

* The event is pushed into a queue (`IAuditEventQueue`).
* This queue decouples the API from the audit logging process.

---

### 3️⃣ Background Processing

* `AuditBackgroundService` runs in the background.
* It continuously:

  * Reads events from the queue.
  * Saves them as `AuditLog` in the database.

---

### ✅ Key Benefit

> The API responds immediately without waiting for audit logging.

---

## 🔄 CQRS Implementation

### Commands (Write)

* `EnrollCourseCommand`
* `EnrollCourseHandler`

### Queries (Read)

* `GetEnrollmentsQuery`

* `GetEnrollmentsHandler`

* Commands modify data.

* Queries only read data.

* Separation improves maintainability and scalability.

---

## 🛠️ Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* BackgroundService
* Clean Architecture
* CQRS Pattern

---

## 🚀 Running the Project Locally

### 1️⃣ Clone Repository

```
git clone https://github.com/your-username/audit-system.git
cd audit-system
```

---

### 2️⃣ Configure Database

Update `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=AuditSystemDb;Trusted_Connection=True;"
}
```

---

### 3️⃣ Apply Migrations

```
dotnet ef database update -s AuditSystem.API -p AuditSystem.Infrastructure
```

---

### 4️⃣ Run the Project

```
dotnet run --project AuditSystem.API
```

---

### 5️⃣ Open Swagger

```
https://localhost:7022/swagger
```

---

## 🧪 Testing the System

### 🔹 Enroll in Course

**POST** `/api/enrollment`

```json
{
  "userId": 1,
  "courseId": 1
}
```

---

### 🔹 Get Enrollments

**GET** `/api/enrollment?userId=1`

---

### 🔍 Check Audit Logs

* Open database
* Query:

```
SELECT * FROM AuditLogs
```

---

## 🔥 Highlights

* Non-blocking audit logging
* Event-driven design
* Clean separation of concerns
* Easily extensible (can replace queue with RabbitMQ/Kafka)

---

## 📌 Future Improvements

* Add Authentication & Authorization
* Use MediatR for CQRS
* Replace in-memory queue with message broker
* Add logging framework (Serilog)
* Add unit & integration tests

---

## 👨‍💻 Author

Developed by Abdullah 🚀
