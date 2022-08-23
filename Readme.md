# Session Logger

Service to log logon, logoff, unlock and lock Events for user on machine

## Publish

https://docs.microsoft.com/de-de/dotnet/core/tools/dotnet-publish

```sh
dotnet publish -c Release --self-contained
```

### Install as Service

https://docs.microsoft.com/de-de/dotnet/core/extensions/windows-service#create-the-windows-service

```sh
sc.exe create "Session Logger Service" binpath="C:\Path\To\sessionLogger.exe"
```