namespace Shorthand.Geodesy.Ellipsoids
{
    public sealed class GRS80 : Ellipsoid
    {
        public GRS80()
        {
            SemiMajorAxis = 6378137d;
            Flattening = 1d / 298.257222101d;
        }
    }
}
