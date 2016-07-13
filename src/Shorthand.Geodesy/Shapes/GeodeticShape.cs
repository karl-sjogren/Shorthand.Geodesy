using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shorthand.Geodesy.Shapes {
    public abstract class GeodeticShape {
        protected List<GeodeticCoordinate> ProtectedShapeCoordinates { get; set; }

        public ReadOnlyCollection<GeodeticCoordinate> ShapeCoordinates => new ReadOnlyCollection<GeodeticCoordinate>(ProtectedShapeCoordinates);
    }
}
