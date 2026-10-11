using System;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            FractalSettings settings = ParseArguments(args);

            Polynomial polynomial = CreatePolynomial();

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomial.Differentiate());

            FractalGenerator generator =
                new FractalGenerator(polynomial);

            Bitmap bitmap = generator.Generate(settings);

            bitmap.Save(settings.OutputPath);
        }

        private static FractalSettings ParseArguments(string[] args)
        {
            if (args.Length < 7)
            {
                throw new ArgumentException(
                    "Expected arguments: width height minX maxX minY maxY outputPath");
            }

            return new FractalSettings
            {
                Width = int.Parse(args[0]),
                Height = int.Parse(args[1]),
                MinX = double.Parse(args[2]),
                MaxX = double.Parse(args[3]),
                MinY = double.Parse(args[4]),
                MaxY = double.Parse(args[5]),
                OutputPath = args[6]
            };
        }

        private static Polynomial CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();

            polynomial.Add(
                new ComplexNumber { Real = 1 });

            polynomial.Add(
                ComplexNumber.Zero);

            polynomial.Add(
                ComplexNumber.Zero);

            polynomial.Add(
                new ComplexNumber { Real = 1 });

            return polynomial;
        }
    }
}
