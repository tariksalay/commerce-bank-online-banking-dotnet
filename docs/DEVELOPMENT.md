# Development and validation

This remains a historical ASP.NET Core 2.2 coursework prototype. The runtime/dependency stack is not upgraded by this change; a supported-framework migration needs build and application-level testing with the matching MySQL schema/stored procedures.

Database operations now create/dispose separate pooled MySQL connections, commands, and readers instead of sharing one mutable connection across requests. Date fields check `DBNull.Value`. Configuration is supplied through the standard `ConnectionStrings:BankDatabase` key; set the `ConnectionStrings__BankDatabase` environment variable or use the project's existing user-secrets mechanism. Use a dedicated local demo database and never commit real connection strings. The former embedded configuration has been removed from the current source; rotate any real credentials that were committed historically.

Example configuration shape (replace placeholders locally):

```text
ConnectionStrings__BankDatabase=Server=localhost;Database=YOUR_DEMO_DB;User=YOUR_DEMO_USER;Password=YOUR_LOCAL_PASSWORD;
```

The original solution path remains `Online Banking/asp-core-mvc.sln`. Restore/build it only in a controlled environment compatible with the legacy SDK. Source/XML validation does not establish a successful .NET build or database integration. Authentication, ownership/authorization checks, password storage, antiforgery protection, and a supported target framework need a separate full migration before this could be considered for any deployment. No real banking data or employer code is added.
