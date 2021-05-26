# Welcome to our Ludo Game!
It's an ASP.NET Core Web App built with Razor pages that is backed up by an API in the same solution! To get a quick overview and sense of what we're trying to do here, 

- [Features](#Features)
- [Getting started](#Getting-started)
- [Models](#Models)
- [Requests](#Requests)
- [Database](#Database)

## Features
The API is created to handle VICs (Very Important Characters) from the Star Wars movies and their space ships at various spaceports around our galaxy (and maybe even further). Of course it's only digital, COVID-friendly and the customer can use the API without any physical support. In the following section we will take you through the API step by step from the UI perspective;

1. The customer registrates themselves at arriving, then they can add a parking if they pass all following controls;

- If the customer is a valid VIC.
- If it's a valid VIV (Very Important Vehicle).
- If their space ship fits, sorry Death Star...
- If there's any available parking spots.
- If the customer already has an ongoing parking. Since there's a high demand on our spaceports and they tends to be occupied, we have a limit on 1 parking / VIC.
 
If everything goes smooth the parking gets registrered in the database and the customer doesn't have do to anything until they leave.

2. When the VIC wants to depart they will add a payment by enter their parking id into the API. The API will subtract the arrival time from the current time and return the cost, which by default is 10 SEK / minute. You as a Developer can change this at any time in the PostPayment-method inside the Paymentscontroller. You can probably make the API work for other types of vehicles, maybe cars on planet Earth. 

Of course the VIC can see their current parking or payments by enter the parking id, however they can neither delete nor update a parking since we don't want anyone to be able to park for free and there's no need to update a parking due to departing time gets defined when the VIC wants to leave. 

An additional feature is that an admin-user can add new spaceports, however it's only you as a Developer that can add admins (set the property IsAdmin to "True" on the specific user in the database), after all we're most powerful creatures in the galaxy, aight? 

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

Endpoints | Description | Status Code | Description |
| :--- | :--- |
| 200 | `OK` |
| 201 | `CREATED` |
| 400 | `BAD REQUEST` |
| 404 | `NOT FOUND` |
| 500 | `INTERNAL SERVER ERROR` |


## Database
This is a diagram of the Database:
[dbdiagram](https://user-images.githubusercontent.com/43240053/117457965-103ff600-af4a-11eb-94c4-2aeacb44dfc5.png)

This is how the Users table look like in the database with some data:

![users](https://user-images.githubusercontent.com/43240053/117458434-978d6980-af4a-11eb-829b-56178d98a481.png)

This is how the Spaceports table look like in the database with some data:

![Spaceports](https://user-images.githubusercontent.com/43240053/117457440-8132de00-af49-11eb-9ff8-5d2c8b657559.png)

This is how the Parkings table look like in the database with some data: 

![parkings](https://user-images.githubusercontent.com/43240053/117457469-87c15580-af49-11eb-8cff-9affc23d604d.png)

This is how the Payments table look like in the database with some data: 

![payments](https://user-images.githubusercontent.com/43240053/117457495-8f80fa00-af49-11eb-8587-d7eecb397c4c.png)

## Status Codes

The API returns the following status codes in its API:

| Status Code | Description |
| :--- | :--- |
| 200 | `OK` |
| 201 | `CREATED` |
| 400 | `BAD REQUEST` |
| 404 | `NOT FOUND` |
| 500 | `INTERNAL SERVER ERROR` |

