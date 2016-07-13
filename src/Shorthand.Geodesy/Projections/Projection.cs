using System;
using Shorthand.Geodesy.Ellipsoids;

namespace Shorthand.Geodesy.Projections {
    public abstract class Projection : IEquatable<Projection> {
        public double CentralMeridianLon { get; protected set; }
        public double CentralMeridianScale { get; protected set; }
        public double LongitudeDelta { get; protected set; }
        public double FalseNorthing { get; protected set; }
        public double FalseEasting { get; protected set; }
        public Ellipsoid Ellipsoid { get; protected set; }

        public bool Equals(Projection other) {
            var cmlEquals = CentralMeridianLon == other.CentralMeridianLon;
            var cmsEquals = CentralMeridianScale == other.CentralMeridianScale;
            var ldEquals = LongitudeDelta == other.LongitudeDelta;
            var fnEquals = FalseNorthing == other.FalseNorthing;
            var felEquals = FalseEasting == other.FalseEasting;

            return cmlEquals & cmsEquals & ldEquals & fnEquals & felEquals & Ellipsoid.Equals(other.Ellipsoid);
        }
    }
}