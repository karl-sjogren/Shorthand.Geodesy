namespace Shorthand.Geodesy.Ellipsoids {
    public sealed class WGS84 : Ellipsoid {
        public WGS84() {
            SemiMajorAxis = 6378137d;
            Flattening = 1d / 298.257223563d;
        }
    }
}
