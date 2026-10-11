using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    internal class FractalGenerator
    {
        private const double ZeroReplacement = 0.0001;
        private const int MaxIterations = 30;
        private const double NewtonStepThreshold = 0.5;
        private const double RootTolerance = 0.01;
        private const int ColorDarkeningPerIteration = 2;

        private readonly Polynomial polynomial;
        private readonly Polynomial derivative;

        private readonly Color[] colorPalette =
        {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Orange,
            Color.Fuchsia,
            Color.Gold,
            Color.Cyan,
            Color.Magenta
        };

        public FractalGenerator(Polynomial polynomial)
        {
            this.polynomial = polynomial;
            derivative = polynomial.Differentiate();
        }

        public Bitmap Generate(FractalSettings settings)
        {
            Bitmap bitmap = new Bitmap(
                settings.Width,
                settings.Height);

            double xStep =
                (settings.MaxX - settings.MinX) / settings.Width;

            double yStep =
                (settings.MaxY - settings.MinY) / settings.Height;

            List<ComplexNumber> roots =
                new List<ComplexNumber>();

            for (int pixelY = 0; pixelY < settings.Height; pixelY++)
            {
                for (int pixelX = 0; pixelX < settings.Width; pixelX++)
                {
                    double x = settings.MinX + pixelX * xStep;
                    double y = settings.MinY + pixelY * yStep;

                    ComplexNumber currentPoint =
                        CreatePoint(x, y);

                    int iterationCount;
                    currentPoint =
                        FindRoot(currentPoint, out iterationCount);

                    int rootIndex =
                        FindRootIndex(currentPoint, roots);

                    Color pixelColor =
                        GetPixelColor(rootIndex, iterationCount);

                    bitmap.SetPixel(
                        pixelX,
                        pixelY,
                        pixelColor);
                }
            }

            return bitmap;
        }

        private ComplexNumber CreatePoint(double x, double y)
        {
            ComplexNumber point = new ComplexNumber
            {
                Real = x,
                Imaginary = y
            };

            if (point.Real == 0)
            {
                point.Real = ZeroReplacement;
            }

            if (point.Imaginary == 0)
            {
                point.Imaginary = ZeroReplacement;
            }

            return point;
        }

        private ComplexNumber FindRoot(
            ComplexNumber currentPoint,
            out int iterationCount)
        {
            iterationCount = 0;

            for (int iteration = 0;
                 iteration < MaxIterations;
                 iteration++)
            {
                ComplexNumber newtonStep =
                    polynomial
                        .Evaluate(currentPoint)
                        .Divide(derivative.Evaluate(currentPoint));

                currentPoint =
                    currentPoint.Subtract(newtonStep);

                if (Math.Pow(newtonStep.Real, 2) +
                    Math.Pow(newtonStep.Imaginary, 2)
                    >= NewtonStepThreshold)
                {
                    iteration--;
                }

                iterationCount++;
            }

            return currentPoint;
        }

        private int FindRootIndex(
            ComplexNumber root,
            List<ComplexNumber> roots)
        {
            for (int index = 0; index < roots.Count; index++)
            {
                double distance =
                    Math.Pow(root.Real - roots[index].Real, 2) +
                    Math.Pow(root.Imaginary - roots[index].Imaginary, 2);

                if (distance <= RootTolerance)
                {
                    return index;
                }
            }

            roots.Add(root);

            return roots.Count - 1;
        }

        private Color GetPixelColor(
            int rootIndex,
            int iterationCount)
        {
            Color baseColor =
                colorPalette[rootIndex % colorPalette.Length];

            int darkening =
                iterationCount * ColorDarkeningPerIteration;

            int red = Math.Max(0, baseColor.R - darkening);
            int green = Math.Max(0, baseColor.G - darkening);
            int blue = Math.Max(0, baseColor.B - darkening);

            return Color.FromArgb(red, green, blue);
        }
    }
}
