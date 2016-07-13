using Shorthand.Geodesy.Ellipsoids;

namespace Shorthand.Geodesy.Projections {
    public class RT90_25GonO : Projection {
        public RT90_25GonO() {
            Ellipsoid = new GRS80();
            CentralMeridianLon = 20d + 18.379d/60d;
            CentralMeridianScale = 1.0000052d;
            FalseNorthing = -670.706d;
            FalseEasting = 1500102.765d;
        }
    }
}