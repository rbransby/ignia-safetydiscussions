# Ignia Safety Discussions Prototype

This repository is my response to the Ignia Safety Discussion prototype problem.

The prototype allows a user to record a safety discussion they've had with a colleague and also review all their previous discussions.

## Assumptions

*   No requirement to view other users submissions (though could be easily extended to include this)
*   No requirement to delete submissions (again, easily added)
*   No offline support required.   

## Packages / Libraries used

*   ASP.Net Core
*   Entity Framework Core 
*   Polymer Web Components

Instructions provided below to (hopefully easily) automagically install the necessary dependencies.

## How to Install

*   Install ASP.NET Core: https://www.microsoft.com/net/core#windows
*   Install bower (package manager for the front end packages)
*   Clone this repo
*   Run 'dotnet restore' to grab the necessary nuget packages for the backend
*   Run 'bower install' to pull down the necessary front-end libraries.
*   Run 'dotnet run' to fire up a mini-web-server and serve up the app
*   Navigate to http://localhost:5000 to check things out.

## Prototype TODO:

As this is a prototype there are obvious limitations that would need to be addressed prior to a production deployment:
*   Persistance - currently the Data is stored temporarily using the Entity Framework In-Memory provider. 
*   Authorization/Authentication - A mechanism to allow a user to authenticate. Currently there is no security implemented at all.
*   Design/Style/Beautification - It looks pretty ugly at the moment :)
*   Locations are currently freeform - might be useful to pre-fill 
