# 🧪 Super-QA

**Super-QA** is an advanced software testing management system built using **ASP.NET Boilerplate (ABP Classic)**. It helps QA teams organize, plan, and execute their testing strategies with clarity, traceability, and efficiency.

---
### 🔑 Key Features

#### ✅ Hierarchical Feature Management
- Define and manage a nested tree of product features.
- Support parent-child relationships between features.
- Features are edition-aware and configurable per tenant.

#### 📎 Feature Attachments & Integrations
- Attach documents or reference external systems like Jira, Azure DevOps, and GitHub.
- Maintain traceability between test assets and development tasks.

#### 🧪 Scenario-Based Test Case Management
- Every **Feature** can contain multiple **Scenarios**.
- Each **Scenario** can have multiple **Test Cases** linked to it.
- Test Cases support step-by-step instructions, test data, and expected outcomes.

#### 📋 Test Plans
- Organize test efforts into reusable plans:
  - Functional Testing
  - Regression
  - Performance
  - Load Testing
- Include relevant features, scenarios, and test cases.

#### 🚀 Test Runs
- Execute tests from a test plan or ad hoc by selecting features, scenarios, and test cases.
- Track results, progress, execution status, and issues.
- Define **test data at each test step level**.

#### 🔌 External Test Framework Integrations
- Integrate with frameworks like **JUnit**, **JMeter**, **Selenium**, etc.
- Ingest test result data via APIs or service hooks.
- Store metadata such as:
  - Test ID
  - Execution time
  - Status
  - Source system
  - Logs and output

#### 🧾 Release Reports
- Link test runs and results to specific releases across environments:
  - Dev
  - UAT
  - Staging
  - Production
- Generate and attach environment-specific test reports to releases for audit and compliance.

#### 👥 Project-Scoped Team Management
- Each tenant can manage **multiple projects**.
- Define team roles and access control at the project level.
- A user can have different roles per project (e.g., Admin in one, Tester in another).

#### 🔐 External Login Providers
- Support user authentication via **Microsoft**, **Google**, and **GitHub**.
- Allow linking external identities to existing users.
- Enable smooth onboarding via OAuth/OpenID Connect integrations.

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
  - Entities and DTOs (ensure all DTOs follow naming and mapping conventions)
  - Application and Domain Services
  - Swagger annotations
  - Unit tests (xUnit + Moq)
- Leverage `AsyncCrudAppService` in application services to auto-generate APIs for entities and support dynamic routing.
- Ensure all generated services and DTOs:
  - Support **pagination**, **filtering**, and **sorting** out-of-the-box using `PagedResultDto`, `PagedAndSortedResultRequestDto`, and query expressions.
  - Are documented and versioned appropriately.
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
- Support **dynamic entity properties**:
  - Allow tenants to define additional fields for entities (e.g., custom attributes for test cases or features).
  - Store dynamic fields in a structured format (e.g., JSON columns or related tables).
  - Provide APIs to manage dynamic field definitions and values per tenant.
  - Ensure these dynamic properties are accessible and filterable via both API and frontend forms.

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

#### ⏱️ Background Jobs with Hangfire

- Integrate **Hangfire** as the background job scheduler for task management.
- Configure Hangfire dashboard for administrative monitoring.
- Use recurring jobs and delayed jobs to handle:
  - Auto-closing stale test runs
  - Sending notifications/reminders
  - Report generation or scheduled data cleanup
- Ensure tenant context is preserved across job execution in a multi-tenant environment using scoped or custom job filters.
- Use dependency injection within jobs to access scoped services and repositories.

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
- Entity Framework Core with **MySQL** provider
- Entity Framework Core
- Angular 16+ for frontend
- Cursor AI in VS Code or Cursor IDE
- xUnit, Moq for testing
- Docker / Docker Compose for local development

---

This architecture ensures scalable, testable, and maintainable development while leveraging the productivity of AI-assisted tooling.
