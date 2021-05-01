# Extensions.Configuration.ConfigurationManager
Provides access to configuration manager files but through Microsoft.Extensions.Configuration abstractions.

[![.NET](https://github.com/ecordovas/Extensions.Configuration.ConfigurationManager/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ecordovas/Extensions.Configuration.ConfigurationManager/actions/workflows/dotnet.yml)

## Considerations

This package was created to help people to step forward in the migration of their code bases to .net core

It could be useful in scenarios where there is no possibility to step away from System.Configuration.ConfigurationManager like:

* Your code has dependencies to third-party packages depending on System.Configuration.ConfigurationManager
* When is not possible to migrate a configuration file with a big list of settings to JSON format

## Recommendation

If your use case is not listed in the _Considerations_ section, the recommendation is to start using **Microsoft.Extensions.Configuration.Json** with **appsettings.json** file, even in the case of .net framework applications.
