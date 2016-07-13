using Shorthand.Geodesy.Ellipsoids;

namespace Shorthand.Geodesy.Projections {
    public class RT90_00GonV : Projection {
        public RT90_00GonV() {
            Ellipsoid = new GRS80();
            CentralMeridianLon = 18d + 3.378d/60d;
            CentralMeridianScale = 1.0000054d;
            FalseNorthing = -668.844;
            FalseEasting = 1500083.521d;
        }
    }
}