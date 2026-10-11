using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{

    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void Add(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

        public Polynomial Differentiate()
        {
            Polynomial derivative = new Polynomial();
            for (int exponent = 1; exponent < Coefficients.Count; exponent++)
            {
                derivative.Coefficients.Add(Coefficients[exponent].Multiply(new ComplexNumber() { Real = exponent }));
            }

            return derivative;
        }

        public ComplexNumber Evaluate(double x)
        {
            return Evaluate(new ComplexNumber()
            {
                Real = x,
                Imaginary = 0
            });
        }

        public ComplexNumber Evaluate(ComplexNumber x)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber powerValue = x;
                int exponent = i;

                if (i > 0)
                {
                    for (int j = 0; j < exponent - 1; j++)
                        powerValue = powerValue.Multiply(x);

                    coefficient = coefficient.Multiply(powerValue);
                }

                result = result.Add(coefficient);
            }

            return result;
        }

        public override string ToString()
        {
            string result = "";
            int i = 0;
            for (; i < Coefficients.Count; i++)
            {
                result += Coefficients[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        result += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    result += " + ";
            }
            return result;
        }
    }
}
