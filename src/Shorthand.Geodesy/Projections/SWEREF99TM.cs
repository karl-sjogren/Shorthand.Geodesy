namespace Shorthand.Geodesy.Projections {
    public class SWEREF99TM : SWEREF99 {
        public SWEREF99TM() {
            CentralMeridianLon = 15d;
            CentralMeridianScale = 0.9996d;
            FalseNorthing = 0d;
            FalseEasting = 500000d;
        }
    }
}