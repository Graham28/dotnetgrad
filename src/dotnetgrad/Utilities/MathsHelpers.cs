using System;
namespace dotnetgrad.Utilities
{
	public static class MathsHelpers
	{
        public static double GetRandomDoubleWithNormalDistribution(double mean, double standardDeviation)
        {
            var random = new Random();
            var u1 = 1 - random.NextDouble();
            var u2 = 1 - random.NextDouble();
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            return mean + standardDeviation * randStdNormal;
        }
    }
}

