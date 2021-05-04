# Extensions.Configuration.ConfigurationManager
Provides access to configuration manager files but through Microsoft.Extensions.Configuration abstractions.

[![Build](https://github.com/ecordovas/Extensions.Configuration.ConfigurationManager/actions/workflows/build-release.yml/badge.svg)](https://github.com/ecordovas/Extensions.Configuration.ConfigurationManager/actions/workflows/build-release.yml)
[![NuGet stable](https://img.shields.io/nuget/v/Extensions.Configuration.ConfigurationManager.svg?style=flat&logo=nuget)](https://www.nuget.org/packages/Extensions.Configuration.ConfigurationManager)

## Getting started

```csharp
 IConfiguration config = new ConfigurationBuilder()
                .AddConfigurationManager()
                .Build();

var value1 = config["key1"];
var value2 = config["key2"];
var connectionString = config.GetConnectionString("db");
```

It is also a good idea to add a reference to Microsoft.Extensions.Configuration.Binder in order to use the following extension methods:

```csharp
var value1 = config.GetValue<string>("key1");
var value2 = config.GetValue<bool>("key2");
```

## Considerations

This package was created to help people to step forward in the migration of their code bases to .net core

It could be useful in scenarios where there is no possibility to step away from System.Configuration.ConfigurationManager like:

* Your code has dependencies to third-party packages depending on System.Configuration.ConfigurationManager
* When is not possible to migrate a configuration file with a big list of settings to JSON format

## Recommendation

If your use case is not listed in the _Considerations_ section, the recommendation is to start using **Microsoft.Extensions.Configuration.Json** with **appsettings.json** file, even in the case of .net framework applications.
