# Boilerplate backend for FrontEdge IT. 
This is a base for future backend API projects in FrontEdge IT. It is built to be scalable for enterprise applications.

## Setup
Below are the steps to get the project running.

### Database locally
If you do not intend to run a local database you can skip this step and skip to Running the application.

The development environment is set to use the database boilerplate with user boilerplate and password boilerplate at localhost. 
To be able to use this you need to have MySQL or MariaDB installed with a user like this configured and with permission to write to database boilerplate.

<ol>
  <li> Get MySql up and running
    <ol>
      <li>You need to install MySQL</li>
      <li>You need to be connected/logged in to MySQL Workbench</li>
      <li>open command prompt and navigate to '"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql" -u root -p'</li>
    </ol>
  </li>
  <li> The steps to produce this is to enter the mysql command prompt using one of the following commands:
    <ol>
      <li>For Linux: sudo mysql</li>
      <li>For Windows: "C:\\Program Files\\MySQL\\MySQL Server 8.0\\bin\\mysqladmin" mysql -u root -p</li>
      <li>For MacOS: /usr/local/mysql/bin/mysql -u root -p</li>
    </ol>
  </li>
  <li> Then issue the following series of commands:
    <ol>
      <li>Create the user: CREATE USER 'nameofyourchoice'@'localhost' IDENTIFIED BY 'nameofyourchoice';</li>
      <li>Grant privileges: GRANT ALL PRIVILEGES ON nameofyourchoice.* TO 'nameofyourchoice'@'localhost';</li>
      <li>Reload all privileges: FLUSH PRIVILEGES;</li>
      <li>Make sure to change the connection string to database in appsettings.Development for these "database=boilerplate;UID=boilerplate" to use 'nameofyourchoice' instead</li>  
    </ol>
  </li>
</ol>

The database should not need to be explicitly created as user boilerplate will have permission to create it.

### Running the application and setup
Follow [this guide](https://docs.github.com/en/authentication/connecting-to-github-with-ssh) to set up git with SSH.

1. Clone the repository: git clone git@github.com:FrontEdgeIT/dear-change-backend.git
2. Make sure you have .NET 6 installed. 
3. In order to work with migrations and database correctly dotnet tools have to be installed. Run the following command for installation: ``dotnet tool install --global dotnet-ef``.
4. Change directory into the API folder and run the application with: dotnet run or dotnet watch run (for live update changes).
5. Verify from the console output that the application is connected to database boilerplate if running in the Development environment.
6. Check so that no warnings occur in the terminal at start. If they do, fix them. 

The default environment is Development, which runs against the local database we setup in the previous section. If you do not intend to run in the development environment you can change this in API/Properties/launchSettings by setting ASPNETCORE_ENVIRONMENT to "Staging".

### Changing ``boilerplate`` to use your own custom api naming
When using Rider see: ``https://blog.jetbrains.com/dotnet/2018/11/21/renaming-projects-easy-way-new-refactoring-rider-2018-3/``
When using Visual Studio: ``https://stackoverflow.com/questions/211241/how-can-i-rename-a-project-folder-from-within-visual-studio``

### Setting environment through command line (ASPNETCORE_ENVIRONMENT=Migrate does not work on windows for example)
In order to see how to set environment variables through command line click the following [Link](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-6.0)

## Workflow

### Adding and updating migrations (SHOULD ONLY BE DONE IN PRODUCTION BY SOMEONE WHO HAS EXPERIENCE).
In order to update the production data base create an appsettings.Production.json file with production settings. It is ignored in .gitignore. 

WARNING: This procedure should only be performed by someone that really knows what they are doing, since this can be devastating for production data.

1. Make changes in DbModels.
2. Add your new migration with: ASPNETCORE_ENVIRONMENT=Migrate dotnet ef Migrations add NameOfMigration.
3. It is really important to run this command with the environment set to Migrate as seen above, otherwise the seed-data in the AppDbContext file will be applied to the snapshot, which never is desired.
4. It is also important to set ASPNETCORE_ENVIRONMENT back to Development after migration is done (if you changed this in launchSettings.json for some reason).
5. Double check in the migrations snapshot so that no seed data has been applied.
6. To apply the newly updated snapshot, run the command: dotnet ef database update. Routine on staging and locally is to drop the database in then reseed it with dotnet run.
7. Check the database so that the changes have applied.
8. In MySql workbench take a dump of the current production database data (you find it under server -> data export). This is for safety.
9. To update the production database run the following command ASPNETCORE_ENVIRONMENT=Production dotnet ef database update.
10. Finally, check the production database and make sure that all changes have applied.

### Validation
Validation of incoming data can be made either through checks and throwing exceptions. The ``Validator`` class can
be extended with more checks that could be used in handlers. Fluent validation can be made on incoming objects to verify that data is correct.
Check ``CreateBoilerplateRequestDtoValidator`` and ``CreateBoilerplateRequestDto`` to see how it is used and the related handler where the validate method is called.
In other words your program gets the data through the controller in the ``CreateBoilerplateRequestDto`` object. Once calling validate on that object in the handler
it runs through the rules and throws a bad request exception with message if the data does not pass the validation.
If more business rules are added later to the program the should be placed in the ``Business`` project. You can chain rules
by returning ``this`` which is the current object when validating. This is required when you try to add data to the database, which might not be acceptable depending on other
data in the database for example.

### Adding and using exceptions
Within the CoreServices project exceptions are added in the exception folder and then added and handled in the ``ExceptionMiddleware``
class. See examples of how to do. The middleware is registered in the pipeline in ``program.cs``.

### Adding new db models.
In the domain project under DomainModels our domain models representing the database are added. All rules and configurations should be set there for the models.
The CombinedDbModels folder are for models that combine different tables and are not pure. The addition of new models is done in
``AppDbContext`` class. Check previously added examples. Don't forget to add related seed data. 

### Mapping
We use mapping to tell how an object should be converted to another. ``MappingProfile`` is where you add new mappings between objects.
Conversion can be made in one or both ways, and you can specify rules for specific properties. 
``MapperDependencyInjection`` is responsible for adding mapping to the program. Domain object should not be exposed to the end user,
but instead be mapped to dtos (data transfer objects).

### Dependency injection
Dependency is used to not rely on concrete implementations but rather interfaces. It means that whenever we ask for an interface
in a constructor for an example it gives us the concrete implementation. This is easy to switch later. ``DependencyInjection`` is 
where more dependency injection should be added further on. 

### Adding seed data
More seed data should be added in ``Seed`` further on. As stated above new migrations should always be added in Migrate environment,
in order to not populate the migration files with unnecessary data. If you run locally in development mode, you can drop the database and run the program with
``dotnet ef database drop && dotnet run``. This will reseed recreate and seed the database with the data given in ``Seed``.

### Working with appsettings file
The appsettings file contains all the secrets connected to your application. You have different files for different environments.
Never place production setting files in the project or any secret data that should not be leaked. Appsettings file contains things
such as db connection strings and other variables that need to accessed throughout the program. Check the ``DependencyInjection`` file
and configure examples to see how to make different properties accessible throughout the program.

### To think about regarding the crud rules (commands, queries and request dtos etc)
Commands are used for anything connected to mutating data such as ``Delete, Post and Patch`` while Queries are
used when retrieving data with ``Get``. Follow the already setup controllers to see how it should be done. RequestDtos are 
for incoming data when we are trying to create new data in the database. 

## Rules to follow
- DomainModels should only represent exact db tables. Combined models goes into CombinedDbModels.
- Keep controllers clean. Watch examples.
- Make sure to implement code quality check in pipeline. 
- Never expose domain models. Data should be leveraged back to the consumer through dtos contained by view models.
- Controllers should be specific to what the concern. Same goes with repositories.
- Validate incoming data. Watch ``CreateBoilerplateRequestDtoValidator`` and ``CreateBoilerplateRequestDto`` as examples.
- If more business rules are applied in the future they should be added to the ``Business`` project. CoreServices holds general validation that can be used in handlers or in business rules.

## Identity
Everything connected to Identity starts from the ``IdentityController`` and most is already setup. Reset password through email has to be added.
To change refresh token and access tokens life time, do so in appsettings. If you want to customize password management or claims just follow the flow from controllers to handlers.
Regarding adding roles, store these connected to the user in the database. Then upon login get them (base method added in IdentityRepository) and place them along with other claims in the token.
In the following [guide](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-6.0) you can see how to add Roles and how to require claims to exist in token and how to 
add these policy to the controllers. 

## Formatting
- In order to run formatting dotnet tools have to be installed: [Dotnet tools](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools)
- Run the command ``dotnet format`` in the solution root level.
- In order to personalize and edit formatting settings, this is done in the ``.editorconfig`` file.
- Read more [here](https://github.com/dotnet/format) regarding dotnet format commands.
- See [this guide for help](https://strepto.github.io/Pause/blog/dotnet-format-rider/) in order to format in Rider editor on save.
1. Open Settings -> Tools -> File Watchers
2. Press The “Plus Sign” to Add a Custom watcher
    1. Set the name to “dotnet format on save”
    2. FileType: ```C#```
    3. Scope: ```Open Files```
    4. Program: ```/Users/xxxxxx/.dotnet/tools/dotnet-format```(exchange "xxxxxx" for your account name)
    5. Arguments: ```$SolutionPath$ -v d --include $FileRelativePath$``` <- Link to solution.
    6. Advanced Options
        1. Disabled all advanced option checkboxes.
    7. All other values were left default
3. Press Ok, and Save Settings