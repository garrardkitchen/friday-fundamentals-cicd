# Purpose

This codebase has been created to demonstrate good DevOps practices such as CI, CD and test automation practices.

This project consists of:

- Azure DevOps Pipelines ([**`/.azdo/pipelines`**](.azdo/pipelines/)) - PR, CI & CD 
- Application Code ([**`/src`**](src)) - Web and API
- Test code ([**`/test`**](test)) - Integration (L3), E2E (L4) and unit testing (L0, L1)

These test, and code coverage, are included as pipeline artefacts.

I use ([**`docker-compose`**](docker-compose.yml)) to orchestrate the hosting of the API HTTP Listener so I can assert Integration tests.

# Getting started

## Inner loop

Run [**`.\winterm`**](winterm.ps1) to open Windows Terminal with pre-configured sln tabs

### Testing



#### Unit

Api:

```powershell
cd UnitTesting
dotnet test --filter "TestCategory=L0" -v 4 
dotnet test --filter "TestCategory=L1" -v 4 
dotnet test --filter "TestCategory=L0|TestCategory=L1" -v 4

Web:

```powershell
cd UnitTesting_Web
dotnet test --filter "TestCategory=L0" -v 4 
```

#### Integration

```powershell
cd UnitTestingTesting
dotnet test --filter "TestCategory=L3" -v 4
```

#### E2E

```powershell
cd End2EndTesting
bin/Debug/net6.0/playwright.ps1 install --with-deps
```

```powershell
dotnet test --filter "Category=L4" 
```

### Code Coverage
```powershell
dotnet test --collect "XPlat Code Coverage" --settings .runsettings.xml --logger trx --results-directory ./TestResults /p:Exclude="[*]*Startup%2c[*]*Program"
reportgenerator "-reports:TestResults/**/*.xml" "-targetdir:TestResults\Coverage" -reporttypes:Html --settings .\.runsettings.xml
```

https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/VSTestIntegration.md