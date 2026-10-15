# Code and artifact reference

[Overview](../README.md) · [Walkthrough](PROJECT_GUIDE.md) · [Development guide](DEVELOPMENT.md)

This reference describes the files on this branch. Function names are navigation aids; the walkthrough explains how their inputs, state, and outputs fit together.

## Application source

### [Program](../Online%20Banking/asp-core-mvc/Program.cs)

Web host entry point.

Methods: `Main`, `CreateWebHostBuilder`.

### [Startup](../Online%20Banking/asp-core-mvc/Startup.cs)

Configure database settings, MVC services, sessions, and the request pipeline.

Methods: `ConfigureServices`, `Configure`.

Properties: `Configuration`.

### [AlertsController](../Online%20Banking/asp-core-mvc/Controllers/AlertsController.cs)

Load alerts and handle alert removal.

Methods: `generateAlertsModel`, `Index`, `Remove`, `Error`.

### [HomeController](../Online%20Banking/asp-core-mvc/Controllers/HomeController.cs)

Build the selected-account overview and provide navigation actions.

Methods: `generateHomeModel`, `Index`, `Transactions`, `Alerts`, `Rules`, `Reports`, `About`, `Error`.

### [LoginController](../Online%20Banking/asp-core-mvc/Controllers/LoginController.cs)

Login form, credential validation, session creation, and logout.

Methods: `Index`, `Login`, `Logout`.

### [ReportsController](../Online%20Banking/asp-core-mvc/Controllers/ReportsController.cs)

Build reports and export an EPPlus workbook.

Methods: `generateReportsModel`, `Index`, `Export`, `Error`.

### [RulesController](../Online%20Banking/asp-core-mvc/Controllers/RulesController.cs)

Read/update rules, validate date order, or delete rules when all flags are off.

Methods: `GenerateRulesModel`, `Index`, `Edit`, `Error`.

### [TransactionsController](../Online%20Banking/asp-core-mvc/Controllers/TransactionsController.cs)

Build the selected account’s transaction page.

Methods: `GenerateTransactionsModel`, `Index`, `Error`.

### [Account](../Online%20Banking/asp-core-mvc/Models/Account.cs)

Account type and balance data.

Properties: `AccountType`, `Balance`.

### [Alerts](../Online%20Banking/asp-core-mvc/Models/Alerts.cs)

Alert rows and associated transaction/rule details.

Properties: `AlertID`, `TransId`, `TransDate`, `TransDesc`, `TransType`, `Location`, `Amount`, `Balance`, `Reason`, `Removed`.

### [DatabaseHandler](../Online%20Banking/asp-core-mvc/Models/DatabaseHandler.cs)

Connection configuration and stored-procedure-backed data operations.

Methods: `SetupConnection`, `CreateConnection`, `getAlerts`, `exportAlerts`, `getTransactions`, `getReports`, `getPrevReports`, `getRules`, `setRules`, `deleteRules`, `removeAlert`, `validateUser`, `getAccounts`, `getAccount`.

### [ErrorViewModel](../Online%20Banking/asp-core-mvc/Models/ErrorViewModel.cs)

Request identifier used by the error page.

Properties: `RequestId`.

### [LoginModel](../Online%20Banking/asp-core-mvc/Models/LoginModel.cs)

Login form fields.

Properties: `UserName`, `Password`, `FullName`, `CustomerID`.

### [Reports](../Online%20Banking/asp-core-mvc/Models/Reports.cs)

Rule occurrence summaries and previous-report date/count fields.

Properties: `RuleReport`, `TimesRecently`, `StartDate`, `EndDate`, `AlertsInTimePeriod`.

### [Rules](../Online%20Banking/asp-core-mvc/Models/Rules.cs)

Rule switches, date bounds, categories, and amount thresholds.

Properties: `accountID`, `OutStateTrans`, `rangeTrans`, `startTrans`, `endTrans`, `catTrans`, `catTxt`, `greatTrans`, `greatTransAmt`, `greatDepo`, `greatDepoAmt`, `greatWithdraw`, `greatWithdrawAmt`, `greatBal`, `greatBalAmt`, `lessBal`, `lessBalAmt`.

### [Transactions](../Online%20Banking/asp-core-mvc/Models/Transactions.cs)

Transaction rows shown for an account.

Properties: `TransId`, `TransDate`, `TransDesc`, `TransType`, `Location`, `Amount`, `Balance`.

### [AlertsModel](../Online%20Banking/asp-core-mvc/ViewModels/AlertsModel.cs)

Screen view model combining account selection and the page’s data objects.

Properties: `curAccount`, `accounts`, `alerts`.

### [HomeModel](../Online%20Banking/asp-core-mvc/ViewModels/HomeModel.cs)

Screen view model combining account selection and the page’s data objects.

Properties: `Balance`, `curAccount`, `accounts`, `Alerts`, `Transactions`.

### [ReportsModel](../Online%20Banking/asp-core-mvc/ViewModels/ReportsModel.cs)

Screen view model combining account selection and the page’s data objects.

Properties: `curAccount`, `accounts`, `Reports`, `PrevReports`.

### [RulesModel](../Online%20Banking/asp-core-mvc/ViewModels/RulesModel.cs)

Screen view model combining account selection and the page’s data objects.

Properties: `curAccount`, `accounts`, `rules`.

### [TransactionsModel](../Online%20Banking/asp-core-mvc/ViewModels/TransactionsModel.cs)

Screen view model combining account selection and the page’s data objects.

Properties: `curAccount`, `accounts`, `transactions`.

## Presentation and database artifacts

Razor templates under `Views` consume the controller-built view models. Static CSS/JavaScript under `wwwroot` belongs to presentation, not database access. `Program` creates the web host; `Startup` uses the default Login route and configures a five-minute session timeout.

| Artifact | Role |
| --- | --- |
| [Online Banking/asp-core-mvc.sln](../Online%20Banking/asp-core-mvc.sln) | Visual Studio solution. |
| [Online Banking/asp-core-mvc/asp-core-mvc.csproj](../Online%20Banking/asp-core-mvc/asp-core-mvc.csproj) | Original `netcoreapp2.2` target and package references. |
| [Online Banking/asp-core-mvc/appsettings.json](../Online%20Banking/asp-core-mvc/appsettings.json) | Application settings; the connection string is supplied locally rather than published. |
| [Online Banking/cs451_project.sql](../Online%20Banking/cs451_project.sql) | Alternate pluralized-table schema artifact. |
| [Database/Banking Database.sql](../Database/Banking%20Database.sql) | Singular-table schema and stored-procedure definitions used when planning database integration. |

The two SQL artifacts differ in names and routines; they are not interchangeable migrations. Match every `DatabaseHandler` call to the selected database before testing. Existing session checks do not establish complete ownership authorization; alert removal ordering and other security limitations remain documented in the development guide. Package restore and the solution build, including Razor views, passed with .NET SDK 8.0.426 on October 15, 2026. The legacy target was not upgraded, and unsupported-framework/dependency vulnerability warnings remain. Application runtime and live database integration were not tested; see the [validation details](DEVELOPMENT.md).
