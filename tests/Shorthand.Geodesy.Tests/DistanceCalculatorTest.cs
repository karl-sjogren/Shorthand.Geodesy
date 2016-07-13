using System;
using NUnit.Framework;

namespace Shorthand.Geodesy.Tests {
    [TestFixture]
    public class DistanceCalculatorTest {
        [Test]
        public void SphericalTest() {
            var coordinate1 = new GeodeticCoordinate() { Latitude = 63.83451, Longitude = 20.24655 };
            var coordinate2 = new GeodeticCoordinate() { Latitude = 63.85763, Longitude = 20.33569 };
            double expected = 5.069d;
            double actual = DistanceCalculator.Spherical(coordinate1, coordinate2);
            Assert.AreEqual(Math.Round(expected, 3), Math.Round(actual, 3));
        }
        
        [Test]
        public void HaversineTest() {
            var coordinate1 = new GeodeticCoordinate() { Latitude = 63.83451, Longitude = 20.24655 };
            var coordinate2 = new GeodeticCoordinate() { Latitude = 63.85763, Longitude = 20.33569 };
            double expected = 5.069d;
            double actual = DistanceCalculator.Haversine(coordinate1, coordinate2);
            Assert.AreEqual(Math.Round(expected, 3), Math.Round(actual, 3));
        }
        
        [Test]
        public void CoordFromDistanceTest() {
            var start = new GeodeticCoordinate() { Latitude = 63.83451, Longitude = 20.24655 };
            double bearing = 140d;
            double distance = 1.3d;
            var expected = new GeodeticCoordinate() { Latitude = 63.8256, Longitude = 20.2636 };
            var actual = DistanceCalculator.CoordFromDistance(start, bearing, distance);
            Assert.IsTrue(expected.Equals(actual));
        }
    }
}
