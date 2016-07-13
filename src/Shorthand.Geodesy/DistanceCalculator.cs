using System;
using Shorthand.Geodesy.Extensions;

namespace Shorthand.Geodesy {
    /// <summary>
    /// Helper methods to calculate distances between coordinates.
    /// </summary>
    public static class DistanceCalculator {
        private const double ApproxEarthRadius = 6371d;

        /// <summary>
        /// Calculates the distance between two geodetic coordinates using the Haversine formula.
        /// </summary>
        /// <param name="coordinate1">The first coordinate.</param>
        /// <param name="coordinate2">The second coordinate.</param>
        /// <returns>The distance between the coordinates in kilometers.</returns>
        public static double Haversine(GeodeticCoordinate coordinate1, GeodeticCoordinate coordinate2) {
            double latDelta = (coordinate1.Latitude - coordinate2.Latitude) * (Math.PI / 180d);
            double lonDelta = (coordinate1.Longitude - coordinate2.Longitude) * (Math.PI / 180d);

            double a = Math.Sin(latDelta / 2) * Math.Sin(latDelta / 2) +
                       Math.Cos(coordinate1.Latitude * (Math.PI / 180d)) * Math.Cos(coordinate2.Latitude * (Math.PI / 180d)) *
                       Math.Sin(lonDelta / 2) * Math.Sin(lonDelta / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return ApproxEarthRadius * c;
        }

        /// <summary>
        /// Calculates the distance between two geodetic coordinates using the Spherical law of cosines.
        /// </summary>
        /// <param name="coordinate1">The first coordinate.</param>
        /// <param name="coordinate2">The second coordinate.</param>
        /// <returns>The distance between the coordinates in kilometers.</returns>
        public static double Spherical(GeodeticCoordinate coordinate1, GeodeticCoordinate coordinate2) {
            double d =
                Math.Acos(Math.Sin(coordinate1.Latitude * (Math.PI / 180d)) * Math.Sin(coordinate2.Latitude * (Math.PI / 180d)) +
                          Math.Cos(coordinate1.Latitude * (Math.PI / 180d)) * Math.Cos(coordinate2.Latitude * (Math.PI / 180d)) *
                          Math.Cos(coordinate2.Longitude * (Math.PI / 180d) - coordinate1.Longitude * (Math.PI / 180d)));
            return d * ApproxEarthRadius;
        }

        /// <summary>
        /// Calculates a new coordinate from a bearing and distance from a specified coordinate.
        /// </summary>
        /// <param name="start">The initial coordinate.</param>
        /// <param name="bearing">The bearing from the initial coordinate in degrees.</param>
        /// <param name="distance">The distance from the initial coordinate in kilometers.</param>
        /// <returns>A new geodetic coordinate representing the new point.</returns>
        public static GeodeticCoordinate CoordFromDistance(GeodeticCoordinate start, double bearing, double distance) {
            distance = distance / ApproxEarthRadius;
            bearing *= Math.PI / 180d;

            double latStart = start.Latitude.ToRadians();
            double lonStart = start.Longitude.ToRadians();

            var latEnd = Math.Asin(Math.Sin(latStart) * Math.Cos(distance) +
                                  Math.Cos(latStart) * Math.Sin(distance) * Math.Cos(bearing));
            var lonEnd = lonStart + Math.Atan2(Math.Sin(bearing) * Math.Sin(distance) * Math.Cos(latStart),
                                         Math.Cos(distance) - Math.Sin(latStart) * Math.Sin(latEnd));
            lonEnd = (lonEnd + 3 * Math.PI) % (2 * Math.PI) - Math.PI;  // normalise to -180...+180

            return new GeodeticCoordinate() { Latitude = latEnd.ToDegrees(), Longitude = lonEnd.ToDegrees() };
        }
    }
}
