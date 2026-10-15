# Development and validation

This remains a historical ASP.NET Core 2.2 coursework prototype. The runtime/dependency stack is not upgraded by this change; a supported-framework migration needs build and application-level testing with the matching MySQL schema/stored procedures.

Database operations now create/dispose separate pooled MySQL connections, commands, and readers instead of sharing one mutable connection across requests. Date fields check `DBNull.Value`. Configuration is supplied through the standard `ConnectionStrings:BankDatabase` key; set the `ConnectionStrings__BankDatabase` environment variable or use the project's existing user-secrets mechanism. Use a dedicated local demo database and never commit real connection strings. The former embedded configuration has been removed from the current source; rotate any real credentials that were committed historically.

Example configuration shape (replace placeholders locally):

```text
ConnectionStrings__BankDatabase=Server=localhost;Database=YOUR_DEMO_DB;User=YOUR_DEMO_USER;Password=YOUR_LOCAL_PASSWORD;
```

The original solution path remains `Online Banking/asp-core-mvc.sln`. Restore/build it only in a controlled environment that can resolve the legacy target and dependencies.

## Build validation — October 15, 2026

Package restore and `Build` succeeded with .NET SDK 8.0.426 / MSBuild 17.11.69, producing both `asp-core-mvc.dll` and `asp-core-mvc.Views.dll`. This also checks Razor compilation. The project still targets `netcoreapp2.2`; no framework or package upgrade was made.

The sandbox prevented the normal SDK CLI startup, so validation invoked the SDK's MSBuild entry point directly:

```bash
DOTNET_ROOT="$SDK_ROOT" MSBuildEnableWorkloadResolver=false \
  "$SDK_ROOT/dotnet" "$SDK_ROOT/sdk/8.0.426/MSBuild.dll" \
  "Online Banking/asp-core-mvc.sln" -restore -t:Build -verbosity:minimal
```

`SDK_ROOT` refers to a local .NET SDK 8.0.426 installation. Build output included `NETSDK1138` for the unsupported target and `NU1902`/`NU1903` vulnerability warnings for the 2.2 framework dependencies. This is compile validation, not confirmation of a secure or supported runtime. No application server was started, no database credentials were used, and MySQL/stored-procedure integration was not exercised.

## Remaining work

 Authentication, ownership/authorization checks, password storage, antiforgery protection, and a supported target framework need a separate full migration before this could be considered for any deployment. No real banking data or employer code is added.
