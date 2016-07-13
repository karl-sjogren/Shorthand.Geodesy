using System;

namespace Shorthand.Geodesy.Ellipsoids {
    public abstract class Ellipsoid : IEquatable<Ellipsoid> {
        public double SemiMajorAxis { get; protected set; }
        public double Flattening { get; protected set; }

        public bool Equals(Ellipsoid other) {
            var smaEquals = Math.Abs(SemiMajorAxis - other.SemiMajorAxis) < Double.Epsilon;
            var fEquals = Math.Abs(Flattening - other.Flattening) < Double.Epsilon;

            return smaEquals & fEquals;
        }
    }
}
