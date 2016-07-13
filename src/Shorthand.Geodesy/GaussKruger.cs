using System;
using Shorthand.Geodesy.Extensions;
using Shorthand.Geodesy.Projections;

namespace Shorthand.Geodesy {
    /// <summary>
    /// Implementation of the Gauss-Krüger formula for converting between geodetic coordinates and grid coordinates.
    /// </summary>
    public static class GaussKruger {
        /// <summary>
        /// Converts a geodetic coordinate to a grid coordinate.
        /// </summary>
        /// <param name="coordinate">The geodetic coordinate to convert.</param>
        /// <param name="projection">The projection to use in the conversion.</param>
        /// <returns>The grid representation of the initial coordinate.</returns>
        public static GridCoordinate GeodeticToGrid(GeodeticCoordinate coordinate, Projection projection) {
            var gridCoordinate = new GridCoordinate { Projection = projection };

            double flattening = projection.Ellipsoid.Flattening;
            double axis = projection.Ellipsoid.SemiMajorAxis;

            double eSquare = flattening * (2d - flattening);
            double n = flattening / (2d - flattening);
            double a = axis / (1d + n) * (1 + n * n / 4 + n * n * n * n / 64d);

            // ReSharper disable InconsistentNaming
            double A = eSquare;
            double B = (5d * eSquare * eSquare - eSquare * eSquare * eSquare) / 6d;
            double C = (104d * eSquare * eSquare * eSquare - 45d * eSquare * eSquare * eSquare * eSquare) / 120d;
            double D = (1237d * eSquare * eSquare * eSquare * eSquare) / 1260d;
            // ReSharper restore InconsistentNaming

            double beta1 = n / 2d - (2d * n * n) / 3d + (5d * n * n * n) / 16d + (41d * n * n * n * n) / 180d;
            double beta2 = (13d * n * n) / 48d - (3d * n * n * n) / 5d + (557d * n * n * n * n) / 1440d;
            double beta3 = (61d * n * n * n) / 240d - (103d * n * n * n * n) / 140d;
            double beta4 = (49561d * n * n * n * n) / 161280d;

            double radLat = coordinate.Latitude.ToRadians();
            double radLon = coordinate.Longitude.ToRadians();
            double radMeridian = projection.CentralMeridianLon.ToRadians();
            double radLonDelta = radLon - radMeridian;

            double conformalLatitude = radLat - Math.Sin(radLat) * Math.Cos(radLat) * (A + B * Math.Pow(Math.Sin(radLat), 2d) + C * Math.Pow(Math.Sin(radLat), 4d) + D * Math.Pow(Math.Sin(radLat), 6d));

            double xiPrime = Math.Atan(Math.Tan(conformalLatitude) / Math.Cos(radLonDelta)); // Bad naming but there is no name defined for this in the specs
            double etaPrime = Atanh(Math.Cos(conformalLatitude) * Math.Sin(radLonDelta)); // Bad naming but there is no name defined for this in the specs

            gridCoordinate.X = projection.CentralMeridianScale * a * (xiPrime +
                beta1 * Math.Sin(2d * xiPrime) * Math.Cosh(2d * etaPrime) +
                beta2 * Math.Sin(4d * xiPrime) * Math.Cosh(4d * etaPrime) +
                beta3 * Math.Sin(6d * xiPrime) * Math.Cosh(6d * etaPrime) +
                beta4 * Math.Sin(8d * xiPrime) * Math.Cosh(8d * etaPrime)) + projection.FalseNorthing;

            gridCoordinate.Y = projection.CentralMeridianScale * a * (etaPrime +
                beta1 * Math.Cos(2d * xiPrime) * Math.Sinh(2d * etaPrime) +
                beta2 * Math.Cos(4d * xiPrime) * Math.Sinh(4d * etaPrime) +
                beta3 * Math.Cos(6d * xiPrime) * Math.Sinh(6d * etaPrime) +
                beta4 * Math.Cos(8d * xiPrime) * Math.Sinh(8d * etaPrime)) + projection.FalseEasting;

            return gridCoordinate;
        }

        /// <summary>
        /// Converts a grid coordinate to a geodetic coordinate.
        /// </summary>
        /// <param name="coordinate">The grid coordinate to convert.</param>
        /// <returns>The geodetic representation of the initial coordinate.</returns>
        public static GeodeticCoordinate GridToGeodetic(GridCoordinate coordinate) {
            var geoCoord = new GeodeticCoordinate();

            var projection = coordinate.Projection;
            double flattening = projection.Ellipsoid.Flattening;
            double axis = projection.Ellipsoid.SemiMajorAxis;

            double eSquare = flattening * (2d - flattening);
            double n = flattening / (2d - flattening);
            double a = axis / (1d + n) * (1 + n * n / 4 + n * n * n * n / 64d);

            // ReSharper disable InconsistentNaming
            double A = eSquare + eSquare * eSquare + eSquare * eSquare * eSquare + eSquare * eSquare * eSquare * eSquare;
            double B = -(7d * eSquare * eSquare + 17d * eSquare * eSquare * eSquare + 30d * eSquare * eSquare * eSquare * eSquare) / 6d;
            double C = (224d * eSquare * eSquare * eSquare + 889d * eSquare * eSquare * eSquare * eSquare) / 120d;
            double D = -(4279d * eSquare * eSquare * eSquare * eSquare) / 1260d;
            // ReSharper restore InconsistentNaming

            double delta1 = n / 2d - (2d * n * n) / 3d + (37d * n * n * n) / 96d - (n * n * n * n) / 360d;
            double delta2 = (n * n) / 48d + (n * n * n) / 15d - (437d * n * n * n * n) / 1440d;
            double delta3 = (17d * n * n * n) / 480d - (37d * n * n * n * n) / 840d;
            double delta4 = (4397d * n * n * n * n) / 161280d;

            double radMeridian = projection.CentralMeridianLon.ToRadians();
            double xi = (coordinate.X - projection.FalseNorthing) / (projection.CentralMeridianScale * a);
            double eta = (coordinate.Y - projection.FalseEasting) / (projection.CentralMeridianScale * a);

            double xiPrime = xi -
                            delta1 * Math.Sin(2d * xi) * Math.Cosh(2d * eta) -
                            delta2 * Math.Sin(4d * xi) * Math.Cosh(4d * eta) -
                            delta3 * Math.Sin(6d * xi) * Math.Cosh(6d * eta) -
                            delta4 * Math.Sin(8d * xi) * Math.Cosh(8d * eta);
            double etaPrime = eta -
                            delta1 * Math.Cos(2d * xi) * Math.Sinh(2d * eta) -
                            delta2 * Math.Cos(4d * xi) * Math.Sinh(4d * eta) -
                            delta3 * Math.Cos(6d * xi) * Math.Sinh(6d * eta) -
                            delta4 * Math.Cos(8d * xi) * Math.Sinh(8d * eta);

            double conformalLatitude = Math.Asin(Math.Sin(xiPrime) / Math.Cosh(etaPrime));
            double radLonDelta = Math.Atan(Math.Sinh(etaPrime) / Math.Cos(xiPrime));

            double radLon = radMeridian + radLonDelta;
            double radLat = conformalLatitude + Math.Sin(conformalLatitude) * Math.Cos(conformalLatitude) *
                            (A +
                             B * Math.Pow(Math.Sin(conformalLatitude), 2d) +
                             C * Math.Pow(Math.Sin(conformalLatitude), 4d) +
                             D * Math.Pow(Math.Sin(conformalLatitude), 6d));

            geoCoord.Longitude = radLon.ToDegrees();
            geoCoord.Latitude = radLat.ToDegrees();

            return geoCoord;
        }

        internal static double Atanh(double d) {
            return 0.5 * Math.Log((1.0 + d) / (1.0 - d));
        }
    }
}
