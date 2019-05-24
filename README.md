
# SchibstedDemo Solution
# Technical test for backends (.NET)

## Getting Started

This project is an exercise / demonstration for the company Schibsted / Adevinta.

This solution consists of two projects maded in C #, one is the project itself and the other is the test project of the first.

In this exercise, it consists of a web application that shows the use and management of methodologies such as ASP.NET, mvc, web api, use of roles and responses to Http requests.

During the creation of this solution neither styles nor special layouts have been used since it was part of the established requirements.

## Prerequisites

Once downloaded this solution must be opened with Visual Studio 2017 or higher and have the Framework 4.6.1 installed.

The libraries used are:

  - package id="Castle.Core" version="4.3.1" targetFramework="net461" 
  - package id="log4net" version="2.0.8" targetFramework="net461" 
  - package id="Microsoft.AspNet.Mvc" version="5.2.7" targetFramework="net461" 
  - package id="Microsoft.AspNet.Mvc.es" version="5.2.7" targetFramework="net461" 
  - package id="Microsoft.AspNet.Razor" version="3.2.7" targetFramework="net461" 
  - package id="Microsoft.AspNet.Razor.es" version="3.2.7" targetFramework="net461"
  - package id="Microsoft.AspNet.WebApi.Client" version="5.2.7" targetFramework="net461" 
  - package id="Microsoft.AspNet.WebApi.Core" version="5.2.7" targetFramework="net461" 
  - package id="Microsoft.AspNet.WebApi.WebHost" version="5.2.7" targetFramework="net461" 
  - package id="Microsoft.AspNet.WebPages" version="3.2.7" targetFramework="net461" 
  - package id="Microsoft.AspNet.WebPages.es" version="3.2.7" targetFramework="net461" 
  - package id="Microsoft.CodeDom.Providers.DotNetCompilerPlatform" version="2.0.0" targetFramework="net461" 
  - package id="Microsoft.Web.Infrastructure" version="1.0.0.0" targetFramework="net461" 
  - package id="Newtonsoft.Json" version="12.0.2" targetFramework="net461" 
  - package id="System.Runtime.CompilerServices.Unsafe" version="4.5.2" targetFramework="net461" 
  - package id="System.Threading.Tasks.Extensions" version="4.5.2" targetFramework="net461" 


These should be created in the **packages** folder in the solution directory.
In the next section we explain how to do it in an automatic way.

## Installing

For the correct installation of the solution we advise, once the project is open, go to Tools-> NuGet Package Administrator-> Manage nuGet packages for the solution and then we execute the update of nonexistent libraries.

## Goals of this application

- Use of singleton classes
- Dependency injection
- SOLID principles
- Use of interfaces (assures to be able to change the behavior of a class without modifying the written code)
- Attributes of authorization of classes for the control of permissions through roles.This application has been done thinking about encapsulation and reuse of code.Any new controller that has the attribute **AuthorizedRolesAttribute** will pass the user role control.
- SessionBaseController is the base class of session control. Any new class that inherits from **SessionBaseController** at the moment that there is no or the session has been lost will be directed to the Login page.
- Help page for web api controller (User). Additionally, the **WebApiTestClient** library has been included for direct testing of this api rest.
- This application has been created so that simply including more roles in the list of roles the application will show and work with them without having to modify anything else (Dynamic execution).
- Use of string resources (application ready for change the language easierly.
- Internally and like demostration mode, controllers calls are made either directly or by Http requests.
- Log4net library for log control.
- In this solution a project of Unit Test is included. Several test are included organized in two folders that show the type of Tests (TDD / BDD).

## Running the tests

Explain how to run the automated tests for this system

## Explain what these tests test and why

Give an example

## And coding style tests

## Built/Deployment With

•Microsoft Visual Studio Community 2017 and c# - The programing framework used
• ,NET Framework 4.6.1 - Target Platform
• IIS Express - Web server

## Authors

• Pep Vivó (pep.vivo@schibsted.com)

## License

This project hasn't license.

