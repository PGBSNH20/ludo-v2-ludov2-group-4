# Welcome to Kevin & Calles Ludo Game!
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
- If you're trying to add a fifth player, error message: "A ludo game can only include".
- If you're trying to enter an invalid color (only 'red', 'green', 'blue' or 'yellow' allowed), error message: "Invalid color".
- If you're choosing an already picked color, error mesage: "Color is occupied".
- If you're trying to enter a nickname which already exists, error message: "The name already exists".

3. Alright, you're ready to go?! You'll be able to launch the game straight away by clicking the at the "Start game" button below the form or you can wait until the night when you're competitors are sleeping and then click at the "Load game" link in the navbar. 

4. When you're entering the game it will show who's turn it is to roll the die, which is located as a button below the tables. The tables represents the players and their pieces (including their positions).

5. Next page that pops up shows the result of the die and the current player's pieces. The player chooses a piece by clicking the button beside each button, which tells the program which piece to move. 

6. The last page in the game loop confirms that everything went as expected and means that the piece and turn order is updated. When the player has clicked on the button "Continue" the loop is done and the game will go straight back to the step "4.", as long as you don't quit the program.

## Getting started 
Since we are using Docker you can run the API through docker compose anywhere you want, but the API is developed in Visual Studio through C# with great support from Entity Framework and Restsharp:

**Infoga screen shot på filstrukturen**

The solution includes an API and Razor pages project. UnitTest1.cs contains all our tests, we have focused on testing all the endpoints in the controllers. 
The API is the brain which hides and takes care of the logic and connection to the database. Razor pages handles the frontend and requests to the API. 

**The API contains following folders;**

- `Dependencies`: contains the ASP.NET Core and .NET Core frameworks, and some extra packages like Restsharp.
- `Controllers`: includes a GameController which holds all the methods that responds on the requests from Razor pages to the endpoints. 
- `Data`: consists of two folders (Interfaces and Repository) and a LudoContext class. Interfaces takes care of the contracts for each model and Repositories **Komplettera**
- `Migrations`: keeps the updates of the database. 
- `Models`: defines our objects GameBoard, Piece and Player.
- `Docker`: contains our Dockerfile and Docker compose which transforms the API into a container and makes it magically run anywhere but watch out, it's heavy stuff.

In addition to these folders there are a couple of other important files:

- `Program.cs`:
- `Startup.cs`:

**Razor pages contains following folders;**
- `Dependencies`:
- `wwwroot`:
- `Pages`:

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

**Infoga databasdiagram**

This is how the GameBoard table look like in the database with some data:

**Infoga bild**

This is how the Player table look like in the database with some data:

**Infoga bild**

This is how the Piece table look like in the database with some data:

**Infoga bild**

