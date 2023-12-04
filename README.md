# URL Shortener

## About

**URLShortener** is a web application designed to simplify your digital life by shortening lengthy URLs. With just a few
clicks, you can convert any unwieldy link into a short.

## Technologies

- ASP.NET Core
- NHibernate
- MariaDB

## Installed Packages
- FluentNHibernate 3.3.0
- MySql.Data 8.2.0
- NHibernate 5.4.6
- NHibernate.NetCore 8.0.0

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
