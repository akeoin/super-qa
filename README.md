# 🧪 Super-QA

**Super-QA** is an advanced software testing management system built using **ASP.NET Boilerplate (ABP Classic)**. It helps QA teams organize, plan, and execute their testing strategies with clarity, traceability, and efficiency.

---

## 🔍 Key Features

- ✅ **Hierarchical Feature Management**
  - Define and manage a nested tree of product features.
  - Support for parent-child relationships between features.

- 📎 **Feature Attachments & Integrations**
  - Attach documents or reference external systems like **Jira**, **Azure DevOps**, and **GitHub**.
  - Maintain traceability between test assets and development tasks.

- 🧪 **Test Case Management**
  - Create detailed test cases for every feature.
  - Add step-by-step instructions, expected results, and test data.

- 📋 **Test Plans**
  - Organize test efforts into reusable plans for:
    - Functional Testing
    - Regression
    - Performance
    - Load Testing
  - Include relevant features and test cases.

- 🚀 **Test Runs**
  - Execute tests either from a test plan or ad hoc by selecting features and test cases.
  - Track results, progress, and issues discovered during execution.

---

### 🧪 Architectural/Technical Requirements (using Cursor AI for ASP.NET Boilerplate)

This section outlines the technical expectations for the implementation of **Super-QA**, focusing exclusively on architecture and engineering best practices, utilizing **ASP.NET Boilerplate (ABP Classic)** and **Cursor AI**.

---

#### ✅ Modular Architecture with ABP

Implement a modular structure using ABP's built-in module system.

---

#### 🏢 SaaS Multi-Tenancy with Isolated Databases

- Design the system to operate as a **SaaS platform** using ABP's multi-tenancy features.
- Each tenant should be completely isolated by using a **dedicated database per tenant**.
- Use `ITenantConnectionStringResolver` to dynamically resolve connection strings at runtime.
- Ensure tenant separation at application, data access, and job execution layers.
- Features available per tenant should be configurable based on **subscription plan or edition**.
- Use `IFeatureChecker` and `IFeatureValueStore` to restrict or allow access to feature-specific functionality.

---

#### ⚙️ Tenant-Specific Settings (Email, SMTP, etc.)

- Use ABP's `ISettingManager` to manage settings per tenant.
- Define and configure:
  - SMTP credentials
  - Email templates and sender names
  - Notification preferences
- Ensure settings fallback to defaults where tenant settings are not configured.

---

#### 📦 Containerized Deployment Support

- Structure the solution to support **Docker-based containerization**.
- Use Docker Compose for local development orchestration (DB + API + Angular).
- Prepare for Kubernetes or cloud-native deployment with environment variables for tenant-specific configurations.
- Create Dockerfiles for:
  - API Host (ASP.NET Core)
  - Angular Frontend
  - Seed and migration runners (optional)

---

#### 🤖 AI-Assisted Development with Cursor

- Use **Cursor AI** to assist in code generation for:
  - Entities and DTOs
  - Application and Domain Services
  - Swagger annotations
  - Unit tests (xUnit + Moq)
- Mark AI-generated blocks with:
  ```csharp
  // Generated with Cursor AI
  ```
- Maintain a `cursor-reflection.md` with:
  - Prompts used
  - Output accepted vs. modified
  - Challenges encountered

---

#### 🧠 Backend Design Principles

- Apply **DDD principles** to separate domain logic from application services.
- Use **UoW & Repositories** via ABP:
  - Custom business logic should leverage `IRepository<T>` and `IUnitOfWorkManager`
- Maintain service abstraction to enable testability and inversion of control.

---

#### 🔐 Authorization System

- Apply ABP's permission model using `PermissionDefinitionProvider`.
- Define granular permissions for:
  - Creating/updating test plans, test runs
  - Executing test runs
  - Managing feature hierarchy
- Use `[AbpAuthorize]` and `IPermissionChecker` for enforcement.

---

#### 📊 Audit Logging and Change Tracking

- Enable full audit logging globally through `AuditingConfigurer`.
- Track:
  - Feature/test creation & updates
  - Test run executions
  - User and role activities

---

#### ⏱️ Background Jobs (Optional but Recommended)

- Use `IBackgroundJobManager` for:
  - Auto-closing stale test runs
  - Sending notifications/reminders

---

#### 🌐 OpenAPI (Swagger) Integration

- Enable Swagger using **Swashbuckle** or **NSwag**.
- Organize endpoints per module for clarity.
- Annotate DTOs and controllers with XML comments for documentation.
- Expose all ABP dynamic APIs using `DynamicApiControllerBuilder`.

---

#### ⚡ Angular Frontend Codegen with OpenAPI

- Export Swagger/OpenAPI spec from backend.
- Use [NSwag](https://github.com/RicoSuter/NSwag) or `openapi-generator-cli` to generate Angular clients:
  ```bash
  nswag openapi2tsclient /input:swagger.json /output:src/app/shared/api.ts
  ```
- Generated clients should power:
  - Feature tree views
  - Test case CRUD
  - Test plan management
  - Test run execution workflows

---

#### 🧪 Unit Testing with xUnit & Cursor AI

- Write test cases for all application and domain services.
- Maintain **>80% coverage** on core logic.
- Use **Cursor AI** to assist in scaffolding test methods.
- Use Moq for mocking external services/dependencies.

---

#### 🛠 Tooling & Environment

- .NET 8 or later
- ASP.NET Boilerplate (ABP Classic, not ABP.IO)
- Entity Framework Core
- Angular 16+ for frontend
- Cursor AI in VS Code or Cursor IDE
- xUnit, Moq for testing

---

This architecture ensures scalable, testable, and maintainable development while leveraging the productivity of AI-assisted tooling.


Feature hierarchy logic

Test case validation

Test plan/run workflows

Permission enforcement




