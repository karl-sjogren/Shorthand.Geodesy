namespace Shorthand.Geodesy.Ellipsoids {
    public sealed class Bessel1841 : Ellipsoid {
        public Bessel1841() {
            SemiMajorAxis = 6377397.155d;
            Flattening = 1d / 299.1528128d;
        }
    }
}
