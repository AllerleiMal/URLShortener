# URL Shortener

## About

**URLShortener** is a web application designed to simplify your digital life by shortening lengthy URLs. With just a few
clicks, you can convert any unwieldy link into a short.

## Technologies

- ASP.NET Core
- NHibernate
- MariaDB
- Telerik KendoUI

## Installed Packages
- FluentNHibernate 3.3.0
- MySql.Data 8.2.0
- NHibernate 5.4.6
- NHibernate.NetCore 8.0.0
- Telerik.UI.for.AspNet.Core.Trial 2023.3.1114

## Usage
### Installation
- Clone repository
```
git clone https://github.com/AllerleiMal/URLShortener.git
```
- Move to the solution directory
```
cd URLShortener
```
- Add Telerik package source if you dont have it. Firstly you need to sign up on [Telerik](https://identity.telerik.com/login) to get login creds
```
dotnet nuget add source "https://nuget.telerik.com/v3/index.json" --name "Telerik" --username <your_username> --password <your_password>
```
- Create local MariaDB scheme and add connection string as DefaultConnection in [appsettings.json](https://github.com/AllerleiMal/URLShortener/blob/dev/URLShortener/appsettings.json)
- Build the solution
```
dotnet build
```
### Launching
- After installation move to the project directory
```
cd URLShortener
```
- Run project with https profile
```
dotnet run --launch-profile "https"
```
- Now you can use URLShortener on [https://localhost:7121](https://localhost:7121)
