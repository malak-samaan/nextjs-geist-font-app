# Comprehensive Plan for Full-Stack Accounting System (Manager.io Replica with Subcontractor Management)

## Information Gathered
- Backend C# models provided for core accounting entities (Account, Transaction, Invoice, Customer, Supplier, Employee, Project, Salary, AuditLog, BackupRestore, User, etc.).
- User wants a full replica of Manager.io accounting software with all features.
- Additional requirement for subcontractor management integrated with projects and accounting.
- Frontend project is Next.js with Tailwind CSS and existing UI components.
- Backend to be implemented in C# ASP.NET Core for API and database.
- High performance and maintainability are priorities.

## Plan

### Backend (C# ASP.NET Core)
- Review and correct all provided C# model files.
- Create API controllers for:
  - Accounts (CRUD, hierarchy)
  - Transactions (CRUD, journal entries)
  - Invoices (sales and purchase)
  - Customers and Suppliers
  - Employees and Payroll
  - Projects and Expenses
  - Subcontractor management (contracts, payments)
  - Audit logs and system settings
  - User authentication and authorization (roles, permissions)
- Implement database migrations and seed data.
- Implement business logic and validation.
- Secure API with JWT or similar authentication.
- Provide RESTful endpoints for frontend consumption.

### Frontend (Next.js with Tailwind CSS)
- Create main entry files: `src/app/layout.tsx` and `src/app/page.tsx`.
- Build pages and components for:
  - Dashboard overview with key metrics.
  - Chart of accounts management.
  - Transactions list and entry forms.
  - Invoice management (sales and purchase).
  - Customer and supplier management.
  - Employee and payroll management.
  - Project and subcontractor management.
  - Reports (balance sheet, profit & loss, trial balance).
  - User management and audit logs.
- Use existing UI components for forms, tables, dialogs.
- Implement client-side state management (React Context or Redux).
- Integrate with backend API for data fetching and mutations.
- Ensure responsive design and accessibility.
- Use Google Fonts and black & white modern theme.

### Integration
- Connect frontend to backend API.
- Implement authentication flow.
- Handle error states and loading indicators.
- Optimize performance and SEO.

## Dependent Files to be Edited/Created
- Backend: Models (corrected), Controllers, Services, Data Access, Migrations.
- Frontend: `src/app/layout.tsx`, `src/app/page.tsx`, pages/components under `src/app` and `src/components`.
- Shared: API client utilities, authentication helpers.

## Follow-up Steps
- Confirm plan with user.
- Implement backend models corrections fully.
- Develop backend API endpoints iteratively.
- Develop frontend pages and components iteratively.
- Test integration and functionality.
- Deploy and monitor.

---

Please confirm if you approve this comprehensive plan so I can proceed with implementation.
