using Shorthand.Geodesy.Ellipsoids;

namespace Shorthand.Geodesy.Projections {
    public class RT90_75GonV : Projection {
        public RT90_75GonV() {
            Ellipsoid = new GRS80();
            CentralMeridianLon = 11d + 18.375d/60d;
            FalseNorthing = -667.282d;
            FalseEasting = 1500025.141d;
        }
    }
}