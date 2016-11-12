using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageTools.Utilities
{
    /// <summary>
    /// Represents a rational number
    /// </summary>
    public struct Fraction
    {
        public int Numerator;
        public int Denominator;

        /// <summary>
        /// Constructor
        /// </summary>
        public Fraction(int numerator, int denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        public string ToText()
        {
            return Numerator + ":" + Denominator;
        }

        /// <summary>
        /// Approximates a fraction from the provided double
        /// </summary>
        public static Fraction Parse(double d)
        {
            return ApproximateFraction(d);
        }

        /// <summary>
        /// Returns this fraction expressed as a double, rounded to the specified number of decimal places.
        /// Returns double.NaN if denominator is zero
        /// </summary>
        public double ToDouble(int decimalPlaces)
        {
            if (this.Denominator == 0)
                return double.NaN;

            return System.Math.Round(
                Numerator / (double)Denominator,
                decimalPlaces
            );
        }


        /// <summary>
        /// Approximates the provided value to a fraction.
        /// http://stackoverflow.com/questions/95727/how-to-convert-floats-to-human-readable-fractions
        /// </summary>
        private static Fraction ApproximateFraction(double value)
        {
            const double EPSILON = .000001d;

            int n = 1;  // numerator
            int d = 1;  // denominator
            double fraction = n / d;

            while (System.Math.Abs(fraction - value) > EPSILON)
            {
                if (fraction < value)
                {
                    n++;
                }
                else
                {
                    d++;
                    n = (int)System.Math.Round(value * d);
                }

                fraction = n / (double)d;
            }

            return new Fraction(n, d);
        }
    }
}
