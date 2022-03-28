# WhatBug Issue Tracker

<br/>

WhatBug is a responsive, full-featured issue tracker written in ASP.Net Core 6.

## Technologies

* Frontend with [ASP.Net Core 6 MVC](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-6.0&tabs=visual-studio)
* Security using [ASP.Net Core 6 Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=visual-studio)
* Data access with [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/whatsnew)
* CQRS with [MediatR](https://github.com/jbogard/MediatR)
* Validation with [FluentValidation](https://fluentvalidation.net/)
* Object-Object Mapping with [AutoMapper](https://automapper.org/)
* Humanization with [Humanizer Core](https://github.com/Humanizr/Humanizer)
* UI using [jQuery](https://jquery.com/), [Bootstrap 5](https://getbootstrap.com/docs/5.0/getting-started/introduction/)
* Automated testing with [xUnit](https://xunit.net/), [Moq](https://github.com/Moq/moq4), [Shouldly](https://github.com/shouldly/shouldly)
* Deployment with [Docker](https://www.docker.com/)
* Automated builds with [Jenkins](https://www.jenkins.io/) or [GitHub Actions](https://github.com/features/actions)

# Architecture

WhatBug was designed using a combination of both the **Clean** and **Vertical Slice** architectures along with the **CQRS** pattern.

![WhatBug Architecture](https://github.com/AnysSido/WhatBug/blob/main/.github/architecture.png?raw=true)

## Clean Architecture
WhatBug is heavily based on Clean Architecture. It is divided into layers as shown in the diagram above. Dependencies must always point inwards. Inner layers cannot hold dependencies to outer layers. This provides loose coupling between layers and allows inner layers to be reused.

### A note on EF Core as Repository/UoW
The dependency rule mentioned above has been broken in one place: EF Core. Traditionally Clean Architecture would require implementation of the Repository Pattern; creating application-level repository and UoW ports/interfaces that would be implemented in the infrastructure layer allowing the application to be decoupled from the database.

In my (sometimes controversial) opinion, EF Core is in itself an implementation of the repository pattern with DbSet as repositories and DbContext as Unit of Work, both of which contain a large array of features that would be extremely time consuming to replicate through an additional layer of abstraction. Additionally EF Core supports multiple database providers so switching providers is, while not seamless, relatively simple.

It is for this reason that WhatBug's application layer holds a dependency to EF Core, using DbSet/DbContext methods directly inside handlers and creating tight coupling between the two. This means WhatBug cannot be built without EF Core, a tradeoff that favors time over flexibility. 

It is extremely unlikely that a project of this scope will need to switch to a database provider not supported by EF Core, and the use of Vertical Slice Architecture with CQRS means that individual problematic queries can still be heavily optimised by switching to dedicated in-process read models or even switched to other persistence mechanisms such as NoSQL if required.

### Layers
WhatBug contains the following layers:

#### Domain
This layer contains business entities, exceptions and value types and has no depencency on any other layer.

#### Application
This layer contains the bulk of the application including:
* **Commands & Queries** - Application request definitions that make up the bulk of the external API to be used by outer layers.
* **Command & Query Handlers** - Business logic is implemented via these handlers.
* **App Services** - Additional functionality to be used by this layer or outer layers that is not suited to a command or query.
* **Authentication** - While the actual authentication is handled in the infrastructure layer, WhatBug holds all of its own user information and only requires the external authentication provider to provide the WhatBug User ID of whoever is signed in. This makes it trivial to switch to another provider or authentication mechanism.
* **Authorization** - A custom attribute allows commands and queries to be easily authorized by simply applying the attribute and providing the required permissions. Supports both user-level and project-level permissions using the same attribute and can handle any combination of required permissions. Authorization is performed in a MediatR behavior that wraps all commands and queries and a unit test exists to ensure every command and query has the attribute.
* **Validation** - Implemented with FluentValidation and MediatR behaviors. Supports both critical validation (such as invalid ID) and user-facing validation (such as incorrectly formatted data entry) so both types of validation can be handled in the same place. Will either throw an exception or return an error object with a message for the UI layer to use.
* **Application Settings** - Using the Options pattern supported by .NET.
* **Application Exceptions** - Non-business exceptions such as RecordNotFoundException and AccessDeniedException.
* **Ports** - Interfaces used by the application that require implementation to be provided by the infrastructure layer.
* **EF Core** - As explained above.

#### Infrastructure
This layer contains implementations of the application-layer ports (interfaces) that grant access to external resources required by the application.
* **IdentityAuthenticationProvider** - Implements the IAuthenticationProvider interface using ASP.Net Core 6 Identity and allows the application to perform authentication and account-based actions such as creating new users, signing in and signing out. This implementation uses its own DbContext and data is held in a separate database. Additionally there is no application user data held in the Identity database, it contains only their authentication user data and their application ID. This mapping between authentication user ID and application user ID along with a separate database means it is relatively trivial to switch to another authentication provider as it simply needs to provide the same mapping.
* **CurrentUserService** - Implements the ICurrentUserService and allows the application to know which user is currently signed in. The application is not concerned with how the user is identified within the provider (ID, email, username etc), it simply requires that the provider can provide the WhatBug user ID for whoever is currently signed in along with some basic authentication information such as email address.
* **FileSystemStorageService** - Allows the application to store and retrieve attachments used in project issues. This implementation uses ASP.Net Core 6 to store and serve files from the server. It can easily be switched with a cloud service provider.
* **WhatBugDbContext** - Implements IWhatBugDbContext and provides the application with a configured DbContext for persistence. In production this context uses PostgreSQL and for tests it is configured to use the EF In-Memory Provider.

#### UI
This layer contains the user interface and project wiring, currently written with ASP.Net Core 6 MVC.
* **Controllers, ViewModels & Views**
* **Automated Breadcrumbs** - Breadcrumbs are automatically generated by parsing results from the new ASP.Net Core 6 Routing middleware.
* **File Storage** - Handles file storage for issue attachments.
* **View Locators** - Custom view locators adding support for Vertical Slice project structure.
* **Dependency Injection Entry** - Entry point for dependency injection wiring up all of the project components.
* **Docker Build Configuration** - Contains the dockerfile for building, testing and publishing a deployable docker image.
* **Gulp & WebPack Configuration** - Build scripts for managing javascript and static assets such as bundling and minification.

### Vertical Slice Architecture & CQRS
Used here along with the CQRS pattern, this architecture encapsulates code into "slices", starting at the UI layer and slicing all the way down to the database.

Code in the Application project is organised into feature folders such as projects, priorities and issues. Within these feature folders each business use case is represented by a single command or query. Commands represent write scenarios where persistent data must be modified whereas queries represent read scenarios where no changes are made to persistent data.

Separating features in this way allows greater separation of concerns between business use cases. Because each command/query is contained within its own "silo" or "slice", they can be modified without fear of interfering with other commands/queries. It is also much easier to introduce new business use cases by simply add the required command or query.

Within the WebUI project, rather than folders such as "Controllers", "Views" and "ViewModels" you will instead find a "Features" folder containing all of the functionality supported by the application. These features are broken down in a way that matches how they are implemented in the Application layer; for every view or action inside a feature folder you will find a corresponding command or query in the application.

#### Read vs Write Model
Due to the scope of this application all commands and queries read/write to the same tables in the database. These are typical relational tables that are more suited to write operations than read operations, where data integrity is essential.

Because read operations are typically more frequent and less critical than write operations, it is possible to query tables that are optimised for read operations by reducing the need for complex joins.

The architecture of WhatBug allows for a transition towards a dedicated Read Model should the need arise. If a view such as the project dashboard started to become a performance concern at scale, data for this view could be stored in a table that is optimised for read performance with all relevant data available in a single SELECT query. In-process events can be fired whenever commands are executed that modify the relevant data and subscribers to events these can be used to keep the read model updated. The use of in-process events mean that data should always be consistent between the read and write models and the entire operation can be wrapped in a transaction if required.

It is also possible to move to a completely separate read model using a different storage mechanism entirely by pushing events to an event bus and allowing those events to be read by other processes, however this is a much larger move and introduces concerns such as eventual consistenty where the data in the read model is not always guaranteed to be up to date, but should always eventually get there.

### Building & Deploying
The WebUI project contains a Dockerfile that will build, test and publish a deployable docker image.

The project can be built and deployed using either Jenkins or GitHub Actions.

#### Jenkins
A Jenkinsfile can be found in the .jenkins directory that can be pasted into Jenkins.

I have created a Docker image with a Jenkins build agent pre-installed with the Docker CLI that can be used to build the project. It can be found at [Jenkins Inbound Agent with Docker CLI](https://github.com/AnysSido/jenkins-inbound-docker-agent).

#### GitHub Actions
The Github Action found in the .github directory will build the WhatBug docker image whenever code changes are pushed and will deploy it to an image repository defined in your github account.
