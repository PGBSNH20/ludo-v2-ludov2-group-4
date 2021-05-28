# Welcome to Kevin & Calles Ludo Game!
It's an ASP.NET Core Web App built with Razor pages that is backed up by an API in the same solution! To get a quick overview and sense of what we're trying to do here, we've wrapped up this README-file:

## Table of contents

- [Features](#Features)
- [Getting started](#Getting-started)
- [Database](#Database)
- [Requests](#Requests)

## Features
The essence of the game is to compete against your friends and move all your pieces across the finish line before everyone else. If you end up on the same position as someone else you'll knock their piece back to their base. This is how you play the game:

1. To be able to play a game you need to click on the the "New game" link in the navbar where you'll start off by entering a name for the game. Then you'll be send straight to the page where you add players (be careful to choose your competitors because it can only be four of you). 

2. When everybody have entered a nickname (because your true name is boooring) , a color and the gameBoard-ID. Then you're ready to go!
PSST! If you somehow manage to screw up by starting googling for the closest Domino Pizza you can enter the base-URL followed by the endpoint /players and then you can continue to add players. The form will do following checks to guide you through this process by kindly respond with an error message if anyone if you enter any invalid input:
- If you're trying to add a fifth player, error message: "A ludo game can only include".
- If you're trying to enter an invalid color (only 'red', 'green', 'blue' or 'yellow' allowed), error message: "Invalid color".
- If you're choosing an already picked color, error mesage: "Color is occupied".
- If you're trying to enter a nickname which already exists, error message: "The name already exists".

3. Alright, you're ready to go?! You'll be able to launch the game by clicking at the "Load game" link in the navbar. 

4. When you're entering the game it will show who's turn it is to roll the die, which is located as a button below the tables. The tables represents the players and their pieces (including their positions).

5. Next page that pops up shows the result of the die and the current player's pieces. The player chooses a piece by clicking the button beside each button, which tells the program which piece to move. 

6. The last page in the game loop confirms that everything went as expected and means that the piece and turn order is updated. When the player has clicked on the button "Continue" the loop is done and the game will go straight back to the step "4.", as long as you don't quit the program.

## Getting started 
Since we are using Docker you can run the API through docker compose anywhere you want, but the API is developed in Visual Studio through C# with great support from Entity Framework and Restsharp:

**Infoga screen shot på filstrukturen**

The solution includes an API and Razor pages project. UnitTest1.cs contains all our tests, we have focused on testing all the endpoints in the controllers. 
The API is the brain which hides and takes care of the logic and connection to the database. Razor pages handles the frontend including the requests to the API. 

**The API contains following folders;**

- `Dependencies`: contains the ASP.NET Core and .NET Core frameworks, and some extra packages like Swashbuckle. 
- `Controllers`: includes a GameController which holds all the methods that responds on the requests from Razor pages to the endpoints. 
- `Data`: consists of two folders (Interfaces and Repository) and a LudoContext class. Interfaces takes care of the contracts for each model and Repositories for the data access.
- `Migrations`: keeps the updates of the database. 
- `Models`: defines our objects GameBoard, Piece and Player.
- `Docker`: contains our Dockerfile and Docker compose which transforms the API into a container and makes it magically run anywhere but watch out, it's heavy stuff.

In addition to these folders there are a couple of other important files:

- `Program.cs`: Is basically just holding the CreateHostBuilder-method and calls it when the program starts.
- `Startup.cs`: Contains our Configure-method and ConfigureServices. The ConfigureServices we've added is a DbContext, AddTransient for our repositories and AddCors to make our requests to the API function properly. 

**Razor pages contains following folders;**
- `Dependencies`: includes ASP.NET Core and Entity Frameworks. But also Newtonsofts JSON-package and Restsharp.
- `wwwroot`: contains our css and Javascript-files, and a library folder including bootstrap and jquery. 
- `Pages`: holds all the Pages;
  - **ChoosePiece:** This page shows where the player ends up after the "Roll the die"- button is clicked in PlayGame (step 4.). The page also prints the player's pieces and the player decides which one to choose.
  - **LoadGame:** Displays all the games including properties such as "Created" and "Winner" when user click "Load Game" in the navbar. 
  - **MovePiece:** OnGet this page is processing the move of the piece that the player choosed and the turnorder. It only gives the player one option, to click the button "Continue" which will send the player back to PlayGame.
  - **NewGame:** A simple form where the player creates a new game by writing a name for the game. When he player submits the page will redirect straight to Players-page.
  - **Players:** The form where the user can add players to a game. 
  - **PlayGame:** Main page for the game-loop itself. Displays which player's up next, all players and their pieces. 
  - **Shared:** Contains the HTML that is the same for all pages, such as header and footer.
  - **Winner:** Prints out the winner as soon as the game is over. 

Pages also contains standard cshtml-files; `_ViewImports` (holds the connection to Razor pages and taghelper), `_ViewStart` and `Index.cshtml.` (homepage).

In addition to these folders we have the same standard files as the API; appsettings, Dockerfile, Program and Startup. 


## Database
This is a diagram of the Database:

![DbDiagram](https://user-images.githubusercontent.com/43240053/119961156-07b96900-bfa6-11eb-9146-583bc5cc43f9.png)

This is how the GameBoard table look like in the database with some data:

![DbGameBoard](https://user-images.githubusercontent.com/43240053/119961187-1011a400-bfa6-11eb-8ca2-ea4424bb9d69.png)

This is how the Player table look like in the database with some data:

![DbPlayer](https://user-images.githubusercontent.com/43240053/119961220-19027580-bfa6-11eb-980e-4a5347229b77.png)

This is how the Piece table look like in the database with some data:

![DbPieces](https://user-images.githubusercontent.com/43240053/119961249-1e5fc000-bfa6-11eb-9d3f-47e39fca0558.png)


## Requests

| Verb| URI | MethodName | Success | Failure |
| :--- | :--- | :--- | :--- | :--- |
| POST | api/Game/gameboards | PostGameBoard | Succes | Failure |
| GET | api/Game/get-gameboards/{gameBoardId} | GetGameBoardById | Succes | Failure |
| POST | api/Game/players | PostPlayer | Succes | Failure |
| GET | api/Game/get-gameboard/players/{id} | GetPlayersByGameBoard | Succes | Failure |
| GET | api/Game/get-gameboards | GetGameBoards | Succes | Failure |
| GET | api/Game/pieces-by/{gameId} | GetPiecesByGameId | Succes | Failure |
| GET | api/Game/pieces/{playerId} | GetPiecesByPlayerId | Succes | Failure |
| GET | api/Game/get-piece/{id} | GetPieceById | Succes | Failure |
| PUT | api/Game/pieces | PutPiece| Succes | Failure |
| GET | api/Game/update-piece-position/{pieceId} | UpdatePiecePosition | Succes | Failure |
| GET | api/Game/get-die/{gameBoardId} | PostDieByGameBoardId| Succes | Failure |
| GET | api/Game/nextplayer/{CurrentGameBoardId} | GetNextPlayer | Succes | Failure |

[Back to start](#Table-of-contents)
