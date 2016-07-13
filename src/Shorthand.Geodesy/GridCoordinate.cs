using System;
using Shorthand.Geodesy.Projections;

namespace Shorthand.Geodesy {
    /// <summary>
    /// Represents a coordinate projected on a grid
    /// </summary>
    public class GridCoordinate : IEquatable<GridCoordinate> {
        public double X { get; set; }
        public double Y { get; set; }
        public Projection Projection { get; set; }

        private const double MaximumDelta = 1.5d;

        public bool Equals(GridCoordinate other) {
            double xDelta = X - other.X;
            double yDelta = Y - other.Y;

            bool xEquals = xDelta < MaximumDelta && xDelta > (MaximumDelta * -1);
            bool yEquals = yDelta < MaximumDelta && yDelta > (MaximumDelta * -1);
            bool projectionEquals = Projection.Equals(other.Projection);

            return xEquals & yEquals & projectionEquals;
        }
    }
}
