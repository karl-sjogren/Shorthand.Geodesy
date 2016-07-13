using System;

namespace Shorthand.Geodesy {
    /// <summary>
    /// Represents a geodetic coordinate
    /// </summary>
    public class GeodeticCoordinate : IEquatable<GeodeticCoordinate> {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        private const Int32 EqualityDecimals = 4;

        public bool Equals(GeodeticCoordinate other) {
            var latitudeEquals = Math.Round(Latitude, EqualityDecimals) == Math.Round(other.Latitude, EqualityDecimals);
            var longitudeEquals = Math.Round(Longitude, EqualityDecimals) == Math.Round(other.Longitude, EqualityDecimals);

            return latitudeEquals & longitudeEquals;
        }
    }
}
