using System;
using dotnetgrad.Utilities;

namespace dotnetgrad.tests
{
	public class UtilitiesTests
	{
		public UtilitiesTests()
		{
		}

        [Test]
        public void RandomNormalTest()
        {
            double test = 0;
            for (int i = 0; i < 100; i++)
            {
                test = MathsHelpers.GetRandomDoubleWithNormalDistribution(0, 0.3);
                Assert.That(test, Is.EqualTo(0.0).Within(1.0));
            }

        }
    }
}

