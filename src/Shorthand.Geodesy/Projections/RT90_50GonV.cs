using Shorthand.Geodesy.Ellipsoids;

namespace Shorthand.Geodesy.Projections {
    public class RT90_50GonV : Projection {
        public RT90_50GonV() {
            Ellipsoid = new GRS80();
            CentralMeridianLon = 13d + 33.376d/60d;
            CentralMeridianScale = 1.0000058d;
            FalseNorthing = -667.130;
            FalseEasting = 1500044.695d;
        }
    }
}