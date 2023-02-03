# Properti Calculator

## Installation
+ [.NET 7 SDK](https://dotnet.microsoft.com/download)
+ *[Visual Studio IDE](https://visualstudio.microsoft.com/downloads/)

## Scripts
### Building
```pwsh
dotnet build 
```
### Testing
#### Run tests with common statistics
```pwsh
dotnet test
```

#### Run tests with displayig name and status
```pwsh
dotnet test -l "console;verbosity=normal"
```

#### Run Fitness tests
```pwsh
dotnet test -l "console;verbosity=normal" --filter FullyQualifiedName~Fitness
```

### Running
```pwsh
dotnet run
```
