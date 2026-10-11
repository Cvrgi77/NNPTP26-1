using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return other.Real == Real && other.Imaginary == Imaginary;
            }
            return false;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Real.GetHashCode();
                hash = hash * 23 + Imaginary.GetHashCode();
                return hash;
            }
        }

        public static readonly ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginary = 0
        };

        public ComplexNumber Multiply(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                Real = Real * other.Real - Imaginary * other.Imaginary,
                Imaginary = (Real * other.Imaginary + Imaginary * other.Real)
            };
        }
        public double Magnitude()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }
        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                Real = Real + other.Real,
                Imaginary = Imaginary + other.Imaginary
            };
        }
        public double GetAngleInRadians()
        {
            return Math.Atan(Imaginary / Real);
        }
        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                Real = Real - other.Real,
                Imaginary = Imaginary - other.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        internal ComplexNumber Divide(ComplexNumber other)
        {
            var numerator = this.Multiply(new ComplexNumber() { Real = other.Real, Imaginary = -other.Imaginary });
            var denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

            return new ComplexNumber()
            {
                Real = numerator.Real / denominator,
                Imaginary = (numerator.Imaginary / denominator)
            };
        }
    }
}
