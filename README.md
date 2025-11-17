# Repository Layout
```text
Renewtrak-Interview/
├─ Part A/
├─ ├─ Part A.sln
├─ ├─ PartATests/
│  └─ Part A/                   # console app (puzzles)
│
└─ Part B/
   ├─ Part B.sln
   ├─ PartBTests/
   ├─ frontend-glossary-app/           # React + Vite + TS（SPA）
   │  ├─ package.json
   │  ├─ vite.config.ts
   │  ├─ index.html
   │  ├─ tsconfig.json
   │  ├─ .env                         
   │  └─ src/
   │     ├─ main.tsx
   │     ├─ App.tsx
   │     ├─ index.css                  
   │     └─ components/
   │        ├─ GlossaryForm.tsx
   │        └─ GlossaryTable.tsx
   │
   └─ Part B/                          # ASP.NET Core Web API
      ├─ Controllers/
      ├─ Services/
      ├─ Infrastructure/
      │  ├─ AppDbContext.cs
      │  └─ Repositories/
      ├─ Domain/
      │  ├─ Entities/
      │  ├─ Dtos/
      │  ├─ Exceptions/
      │  └─ Interfaces/
      ├─ Middlewares/
      ├─ Migrations/               # EF Core migrations
      ├─ Program.cs
      ├─ appsettings.json
      ├─ appsettings.Production.json
      └─ appsettings.Development.json.Sample
```
## Production ready
- ✅ This project uses SQLite – **no manual database setup is needed**.
- ✅ On first run, the database will be created and EF Core migrations will apply automatically.
- ✅ Fully deployed for testing:

- 🌐 Frontend: https://proud-cliff-02da6df00.3.azurestaticapps.net
- 🌐Backend API: https://glossaryapp-hnamcxg6f5a9ehe4.australiaeast-01.azurewebsites.net/swagger/index.html

## How to run Part A
- install .NET 9 SDK (https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
```text
cd Part\ A
dotnet restore
dotnet run --project "Part A/Part A.csproj"
```
## How to run – Part B
- install .NET 9 SDK (https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- dotnet tool install -g dotnet-ef
```text
1. Configure(Local) -> copy appsettings.Development.json.Sample to your appsettings.Development.json
2. Run Backend(Local)
    - cd Part\ B/
    - dotnet restore
    - dotnet ef update database (For .NET CLI) | Update-Database (For Visual Studio PowerShell)
    - dotnet run dev
3. Run Frontend(Local)
    - cd frontend-glossary-app
    - npm i
    - npm run dev
```

## Tests
### Part A tests
- xUnit unit tests for puzzle functions (LinkedListPuzzles, WordsReverse).
```text
cd Part\ A
dotnet test  # run xUnit tests for Part A 
```
### Part B tests
- xUnit + Moq + FluentAssertions
- Now only test the service layer (business logic, validation, error handling).
- Repositories are mocked; don’t touch a real DB.
```text
cd Part\ B
dotnet test  # run xUnit tests for Part B 
```
## CI/CD
- Backend (App Service | Basic B1 Plan and setting always on to avoid cold start)
- Frontend (Static Web App | Free Plan)

## Troubleshooting
#### Problem:
- Visiting the Static Web Apps (SWA) URL shows the default “Congratulations on your new site!” page.
- Browser console shows:
  - TypeError: 'application/octet-stream' is not a valid JavaScript MIME type
#### Root cause:
- Oryx build + space in path - The SWA action tried to build from Part B/frontend-glossary-app.
- Missing JS/MJS MIME mapping
####   Fix strategy
- Do not let SWA/Oryx build. Build the app in the subfolder.
- Avoid spaces in the deploy path. Copy the built dist/ to a flat folder named upload/ at the repo root.
- Set explicit MIME types. Ensure staticwebapp.config.json (with JS/MJS mappings) is present at the root of the deployed folder.

### Backend dev: http://localhost:5089/swagger/index.html