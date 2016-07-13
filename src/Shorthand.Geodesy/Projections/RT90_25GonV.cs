using Shorthand.Geodesy.Ellipsoids;

namespace Shorthand.Geodesy.Projections {
    public class RT90_25GonV : Projection {
        public RT90_25GonV() {
            Ellipsoid = new GRS80();
            CentralMeridianLon = 15d + 48d/60d + 22.624306d/3600d;
            CentralMeridianScale = 1.00000561024d;
            FalseNorthing = -667.711;
            FalseEasting = 1500064.274d;
        }
    }
}