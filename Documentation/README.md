# Welcome to our Ludo Game!
It's an ASP.NET Core Web App built with Razor pages that is backed up by an API in the same solution! To get a quick overview and sense of what we're trying to do here, we've wrapped up this README-file into a table of contents:

- [Features](#Features)
- [Getting started](#Getting-started)
- [Models](#Models)
- [Requests](#Requests)
- [Database](#Database)

## Features
The essence of the game is to compete against your friends and move all your pieces across the finish line before everyone else. If you end up on the same position as someone else you'll knock their piece back to their base. This is how you play the game:

1. To be able to play a game you need to click on the the "New game" link in the navbar where you'll start off by entering a name for the game. Then you'll be send straight to the page where you add players (be careful to choose your competitors because it can only be four of you). 

2. When everybody have entered a nickname (because your true name is boooring) , a color and the gameBoard-ID. Then you're ready to go!
PSST! If you somehow manage to screw up by starting googling for the closest Domino Pizza you can enter the base-URL followed by the endpoint /players and then you can continue to add players. The form will do following checks to guide you through this process by kindly respond with an error message if anyone if you enter any invalid input:
- If you trying enter an existing nickname.
- If you're trying to enter an invalid color (only 'red', 'green', 'blue' or 'yellow' allowed).
- If
- If

3. Alright, you're ready to go?! You'll be able to launch the game straight away by clicking the at the "Start game" button below the form or you can wait until the night when you're competitors are sleeping and then click at the "Load game" link in the navbar. 

4. 

## Getting started 
To give you a smooth experience and get to know our the structure of our API we thought it would be a good idea to show some print screens and explain them. Since we are using Docker you can run the API through docker compose anywhere you want, but the API is developed in Visual Studio through C# with great support from Entity Framework and Restsharp:

[filestructure](https://user-images.githubusercontent.com/43240053/117458224-585f1880-af4a-11eb-9817-db0b80f04726.png)

As you can see we have two main projects in the solution; NUnitTestProject and SpaceParkAPI. UnitTest1.cs contains all our tests, we have focused on testing all the endpoints in the controllers. 
Even though our tests hopfeully got you excited we think it's inside the SpaceParkAPI the action takes place.
It consists of a few key folders;

- `Dependencies`: contains the ASP.NET Core and .NET Core frameworks, and some extra packages like Restsharp.
- `Controllers`: includes a controller class for each model. That's where our business logic with all the 
   endpoints sits. 
- `Data`: is the home of our beloved DbContext. Doesn't seem to be much code for the world but without we    would literally loose our connections.
- `Models`: defines our objects Park, Pay, SpacePort, SpaceShip and User.
- `Docker`: contains our Dockerfile and Docker compose which transforms the API into a container and makes it magically run anywhere but watch it, it's heavy stuff.

Except these folders we have the standard Program- and Startup classes. At last we've put our Swapi.cs which holds the requests and valid methods related to the external API from swapi.dev. Which makes it possible for us to enjoy our evenings instead of manually validate every single customer.  

## Models

## Requests

| Endpoints | Description | Status Code | Message |
| :--- | :--- | :--- | :--- |
| Test | Test | 200 | `OK` |
| Test | Test | 201 | `CREATED` |
| Test | Test | 400 | `BAD REQUEST` |
| Test | Test | 404 | `NOT FOUND` |
| Test | Test | 500 | `INTERNAL SERVER ERROR` |


## Database
This is a diagram of the Database:
[dbdiagram](https://user-images.githubusercontent.com/43240053/117457965-103ff600-af4a-11eb-94c4-2aeacb44dfc5.png)

This is how the Users table look like in the database with some data:

![users](https://user-images.githubusercontent.com/43240053/117458434-978d6980-af4a-11eb-829b-56178d98a481.png)

