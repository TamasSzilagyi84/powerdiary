How to run the application locally

## DB
Chat microservice connects to a SQL server on port 5434 using the password: Pass@word
Docker command to run the database: 

`docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Pass@word" -p 5434:1433 -d mcr.microsoft.com/mssql/server:2022-latest`

## BE
The correct API gateways and microservices needs to run. Currently, it is only the **Web.HttpAggregator** and the **Chat.Api** project.
Within the properties of the solution, select multiple startup projects, and change it to debug for the projects mentioned above. 
Make sure it usese the https launch profile from the launchSettings.json

## FE
Run `npm run dev` command

**Launching the components in the right order does matter**! First DB, then BE and lastly the FE.

**TODO** finish docker-compose.yaml or a local K8 cluster

--------------------------------------------------------------------------------------------

## DB Migration for performance testing
The **Chat.DbTestMigrator** project is a console application to create **10M** ChatMessage records in the database. This does take a while 
to finish.

--------------------------------------------------------------------------------------------

# Development

## Request validation
The application uses FluentValidation to validate each request. Please place request validators in the following folder within the API projects.
RequestValidators/{ControllerName}/{RequestClassName}{HttpVerb}Validator.cs

## Settings
Please use the options pattern. Create a class with the right properties that matches the applicationSettings.json file structure, bind the values 
on app startup and inject it where it is needed. See examples: Options/ApplicationSettings.cs, Configuration/OptionsConfiguration.cs,
Middlewares/WebHttpAggregatorErrorHandlingMiddleware.cs

## Services
Services are "registered" on app startup. Please refer to Configuration/ServiceConfiguration.cs

## Exceptions and error handling
Common exception types are placed within the **Shared** project and domain specific exceptions types should be placed within the corresponding {entity}.Domain project.
Error handling should be done within the **ErrorHandlingMiddlewares** within each project.

## Commonly used messages and strings
Please use the **Const.cs** class within the shared project. No hardcoded strings..

## Correlation ID
Each API gateway should have a middleware to inject and to pass on the correlation id. In case of a bug, this can be used to track the whole request flow from the API 
gateways to the microservices and back.

## DB / Domain model updates
The {entity}.Infrastructure or a separate {entity}.Migration project should be used to store the EF Core DB migration scripts. Currently code first approach is used.
DB constraints and DB table related updates should be defined within the corresponding context class.

## Caching layer
To be completed..

## Service bus
To be completed
