# Mimbly dashboard backend

This is the backend for Mimblys dashboard project. It is built to be a scalable API.
Below are the steps to get the project running.

## Deploying the solution

### Required Service

-   Azure AD
-   MSSQL Database
-   Container deployment service

### Azure AD

The project relies on Azure AD for authorization and requires it to validate users from the frontend. This can be reconfigured to use another OAuth provider.

To smoothly get started setup a Azure Active Directory (Ad) and create an app registration for mimbly and acquire the following fields; "Application ID" and "Directory ID". While you are still in your app registration, create a new client secret too.

### Database

The application requires a MSSQL database and can be adapted to other SQL databases by modifying the class `SqlDataAccess.cs` found in the persistance directory.

To smoothly get started setup a MSSQL database and create the database tables by using the provided migration file.
(Don't forget to acquire the connection string)

[Read more about migrating here.](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

### Deploying the container

Setup a container registry to push the dockerized container to. Use your favorite web application container deployment service to deploy the container from the registry.

If you are using Azure to deploy the service, you can use our provided pipeline with ease. Just make sure you have set up the environment variables in the app service configuration.

Check if the endpoint is accessible and voila you have the application running.

### Environment Variables

Make sure to fill in these variables in appsettings.json.

```
"ConnectionStrings": {
    "DbConnectionString": *Your database connection string (duh!)*
  },
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "ClientId": *Your Ad Application ID*,
    "TenantId": *Your Ad Directory ID*,
    "Audience": *Your Ad Application ID*,
    "Issuer": "https://sts.windows.net/*Your Ad Application ID*"
  },
  "ApiKey": *A Secure key (sync it with the frontend)*,
  "CorsUrl": *Frontend Url*
```
