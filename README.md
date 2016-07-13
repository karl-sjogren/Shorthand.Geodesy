# Shorthand.Geodesy
Helper methods to convert between WGS84 and RT90/SWEREF99 and calculate distances between coordinates.

This was earlier published to CodePlex under the name CrazyBeavers Geodesy (also by me) but I've cleaned up the code and renamed it from its old silly name when moving to GitHub.

## Installation

Install the nuget package using ``Install-Package Shorthand.Geodesy`` or clone and reference the project in the repository.

## Usage

### Convert grid to geodetic

```csharp
// Create a new GridCoordinate with a specified projection
var gridCoord = new GridCoordinate { X = 7094946, Y = 1693701, Projection = SwedishProjections.RT90_25GonV };

// Use the GaussKruger-algorithm to convert the GridCoordinate to a GeodeticCoordinate
GeodeticCoordinate geoCoord = GaussKruger.GridToGeodetic(gridCoord);
```

The result of the above calculation should yield a new GeodeticCoordinate with the values of about Latitude = 63.90786 and Longitude = 19.75247.

### Convert geodetic to grid

```csharp
// Create a new GeodeticCoordinate
var geoCoord = new GeodeticCoordinate { Latitude = 63.90786d, Longitude = 19.75247d };

// Use the GaussKruger-algorithm to convert the GeodeticCoordinate to a GridCoordinate with the specified projection
var gridCoord = GaussKruger.GeodeticToGrid(coordinate, SwedishProjections.RT90_25GonV);
```

The result of the above calculation should yield a new GridCoordinate with the values of about X = 7094946 and Y = 1693701.

### Calculating distance between coordinates

```csharp
// Create a pair of coordinates
var coordinate1 = new GeodeticCoordinate { Latitude = 63.83451d, Longitude = 20.24655d };
var coordinate2 = new GeodeticCoordinate { Latitude = 63.85763d, Longitude = 20.33569d };

// Calculate the distance between the coordinates using the haversine formula
double distance = DistanceCalculator.Haversine(coordinate1, coordinate2);
```

The result of the above calculation should be slightly over 5 kilometers.
