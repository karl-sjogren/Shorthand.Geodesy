using Shorthand.Geodesy.Ellipsoids;

namespace Shorthand.Geodesy.Projections {
    public class RT90_50GonO : Projection {
        public RT90_50GonO() {
            Ellipsoid = new GRS80();
            CentralMeridianLon = 22d + 33.380d/60d;
            CentralMeridianScale = 1.0000049d;
            FalseNorthing = -672.557d;
            FalseEasting = 1500121.846;
        }
    }
}