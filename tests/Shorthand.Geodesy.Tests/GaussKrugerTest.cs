using NUnit.Framework;
using Shorthand.Geodesy.Projections;

namespace Shorthand.Geodesy.Tests {
    [TestFixture]
    public class GaussKrugerTest {
        [Test]
        public void AtanhTest() {
            double d = .7d;
            double expected = .86730d;
            double actual = GaussKruger.Atanh(d);
            Assert.That(actual, Is.EqualTo(expected).Within(.000001));
        }

        [Test]
        public void GeodeticToGridTest() {
            var coordinate = new GeodeticCoordinate { Latitude = 63.90786d, Longitude = 19.75247d };
            var projection = SwedishProjections.RT90_25GonV;
            var expected = new GridCoordinate { X = 7094946, Y = 1693701, Projection = SwedishProjections.RT90_25GonV };
            var actual = GaussKruger.GeodeticToGrid(coordinate, projection);
            Assert.IsTrue(expected.Equals(actual));
        }

        [Test]
        public void GridToGeodeticTest() {
            var coordinate = new GridCoordinate { X = 7094946, Y = 1693701, Projection = SwedishProjections.RT90_25GonV };
            var expected = new GeodeticCoordinate { Latitude = 63.90786d, Longitude = 19.75247d };
            var actual = GaussKruger.GridToGeodetic(coordinate);
            Assert.IsTrue(expected.Equals(actual));
        }
    }
}
