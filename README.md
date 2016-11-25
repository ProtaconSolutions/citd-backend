## Prequisities
Install .net core 1.1 preview 1 [https://github.com/dotnet/core/blob/master/release-notes/preview-download.md](https://github.com/dotnet/core/blob/master/release-notes/preview-download.md)

## Getting started
```
dotnet restore
dotnet run
```

##Building Docker container
```
dotnet restore
dotnet publish -c release -r rhel.7-x64
docker build .
```
