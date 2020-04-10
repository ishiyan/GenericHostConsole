# GenericHostConsole

Demonstrates how to create a .Net Core 3.1 console application with a *"Hello, World!"* worker
implementing `IHostedService` hosted in the `GenericHost`.

This allows to use .Net Core dependency injection, logging and configuration in a console application.

Try to change some values in `appsettings.json`.
```json
  "Worker": {
    "greetingText": "Hello, World!",
    "maxIterationCount":  10 
  },
```

Note how we copy `appsettings.json` to the output directory in the project file.

```xml
  <ItemGroup>
    <Content Include="**\*.json" Exclude="bin\**\*;obj\**\*" CopyToOutputDirectory="Always" />
  </ItemGroup>
```
 