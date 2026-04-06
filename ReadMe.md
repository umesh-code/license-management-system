﻿# License Management System

A **multi-tenant, microservices-based License Management System** built with ASP.NET Core. This project demonstrates a scalable architecture using API Gateway, JWT authentication, and clean architecture principles.

---

## Overview

This system is designed to manage professional licenses across multiple government agencies. It supports **multi-tenancy**, **role-based access**, and **secure service communication** through an API Gateway.

---

## Architecture

- **Frontend:** ASP.NET MVC (Role-based UI)  
- **API Gateway:** Ocelot  
- **Microservices:**
  - License Service (CQRS + Hangfire)
  - Document Service (File Management)
  - Notification Service
- **Database:** SQL Server  
- **Authentication:** JWT (Token-based)

---

## Features

### Core Features

- Role-based dashboards (Admin/User)
- Multi-tenant support using `TenantId`
- Secure APIs with JWT authentication
- API Gateway routing via Ocelot
- Document upload and management
- Notification service integration

### Advanced Features

- CQRS pattern using MediatR
- Background jobs with Hangfire (license renewal checks)
- Microservice communication via API Gateway
- Clean architecture with separation of concerns

---

##  Microservices Details

###  License Service
- Create and retrieve licenses
- Implements CQRS using MediatR
- Background job for license expiry (Hangfire)

###  Document Service
- Upload documents (`multipart/form-data`)
- Retrieve documents by `TenantId`
- Stores files locally

### Notification Service
- Sends notifications via API
- Integrated with UI and API Gateway

---

## Authentication

- JWT-based authentication  
- Token endpoint:

```http
POST /api/auth/login
```

## Multi-Tenancy

- Implemented using `TenantId`
- All queries are tenant-filtered
- Indexed `TenantId` for performance optimization

## Technologies Used

- .NET 6 / ASP.NET Core  
- Entity Framework Core  
- SQL Server  
- MediatR (CQRS)  
- Hangfire (Background Jobs)  
- Ocelot API Gateway

### Prerequisites

- Visual Studio 2022+  
- .NET 8 SDK  
- SQL Server 

## How to Run the Project
## Prerequisites

- Visual Studio 2022+  
- .NET 8 SDK  
- SQL Server  

## Run Services in the Following Order
To start the services, make sure to run them in the following order:

	1. **LicenseService**  
	2. **DocumentService**  
	3. **NotificationService**  
	4. **ApiGateway**  
	5. **LicenseUI**

### Setup Steps
## Steps to Run the Project

### 1. Download / Extract Project
- Download or clone the repository.  
- If you have a zipped version, unzip it to a desired folder.

### 2. Open Solution
- Open the solution file (`.sln`) in **Visual Studio 2022+**.

### 3. Update Connection Strings
- Update the connection strings in `appsettings.json` according to your environment:

```json id="7q9vsk"
{
  "ConnectionStrings": {
    "DefaultConnection": "Your-Connection-String-Here"
  }
}
```
### 4. Build Solution
- In Visual Studio, **Build Solution** (`Ctrl + Shift + B`) to restore all NuGet packages and compile all projects.

### 5. Run Services in Order
- Start the projects in the following order:
  1. **LicenseService**  
  2. **DocumentService**  
  3. **NotificationService**  
  4. **ApiGateway**  
  5. **LicenseUI**

### 6. Verify
- Ensure all services start without errors.  
- Access the **Hangfire Dashboard** for background jobs at:
```bash
https://localhost:7058/hangfire
```

## API Gateway
**Base URL:**
```bash id="zvwqr7"
https://localhost:7058
```

## Sample API Requests

### Login to Generate Token
```http id="o7vwz8"
POST https://localhost:7058/auth/login
```
Use the following payload:
``` Sample Value
{
  "username": "umesh",
  "role": "Admin"
}
```
You will receive a JWT token in the response.
Copy this token and add it to the Authorization header as a Bearer Token for all subsequent API requests.

### Create License
```http id="3bnjqd"
POST https://localhost:7058/license
```
### Request Body: 
```JSON:
{
  "tenantId": "gov4",
  "productName": "Product B",
  "expiryDate": "2026-12-31",
  "userId": "user321",
  "licenseType": "Test"
}
```
You should receive a successful response confirming license creation.

### Document Upload API
Before proceeding, keep a text file ready with sample content.
```http id="3bnjqd"
POST https://localhost:7058/document/upload
```
### Request Body: 
```In Postman (form-data):
	file → Type: File → Select your file
	tenantId → Type: Text → Value: gov4
```
You should receive a successful file upload response.

### UI Testing

#### Login to UI
- Open the UI application.
- Enter the following credentials:
  - **Username:** umesh  
  - **Role:** Admin  
- Click **Login**.

---

#### Verify Functionality
- Use `gov4` to:
  - Fetch **License details**
  - Fetch **Document details**
- Responses will be displayed directly in the UI.

---

#### Notifications
- Enter any message and trigger a notification.
- The response will be visible on the UI.

---

#### Role-Based Access Testing
- **Test Authorization**
  - Change the user role (e.g., Admin/User) and verify:
    - Access control behavior  
    - API restrictions based on roles  

---

#### Logout
- Once testing is completed, click on **Logout**.

---

## Notes

- Please use **Postman** for all POST requests. Payloads and required details are provided above.  
- GET requests are integrated into the **UI**, and the responses are displayed there.  
- Hangfire Dashboard is available at:
```bash
https://localhost:7058/hangfire