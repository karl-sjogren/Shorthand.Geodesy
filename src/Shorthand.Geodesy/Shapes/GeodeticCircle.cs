using System;

namespace Shorthand.Geodesy.Shapes {
    public class GeodeticCircle : GeodeticShape {
        public GeodeticCircle(GeodeticCoordinate origin, double radius, Int32 segments) {
            double segmentIncrease = 360d / segments;

            for(Int32 i = 0; i < segments; i++) {
                ProtectedShapeCoordinates.Add(DistanceCalculator.CoordFromDistance(origin, i * segmentIncrease, radius));
            }
        }
    }
}
