using System;

namespace Shorthand.Geodesy.Extensions {
    /// <summary>
    /// Extensions methods for System.Double.
    /// </summary>
    public static class Double {
        /// <summary>
        /// Converts from radians to degrees.
        /// </summary>
        public static double ToDegrees(this double d) {
            return d * (180d / Math.PI);
        }

        /// <summary>
        /// Converts from degrees to radians.
        /// </summary>
        public static double ToRadians(this double d) {
            return d * (Math.PI / 180d);
        }
    }
}
