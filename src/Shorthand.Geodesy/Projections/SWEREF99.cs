using Shorthand.Geodesy.Ellipsoids;

namespace Shorthand.Geodesy.Projections {
    public abstract class SWEREF99 : Projection {
        public SWEREF99() {
            Ellipsoid = new GRS80();
            CentralMeridianLon = double.NaN;
            CentralMeridianScale = 1d;
            FalseNorthing = 0d;
            FalseEasting = 150000d;
        }
    }
}