using System;
using dotnetgrad.Utilities;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

        [Test]
        public void UtilitiesTests_SaveModelAsJson_ThenRetreiveModelFromFile()
        {
            //Arrange
            var input1 = new List<double>() { 0.1, 0.8, -0.7 };
            var mlp = new MultiLayerPerceptron(3, 3, 5, 1);
            mlp.TrainBatch(new List<DataPoint>() { new DataPoint(input1, new List<double>() { 3.0 })});
            var output = mlp.FeedForward(input1);
            var pathToFile = SavingAndRetreiving.SaveModelAsJson(mlp, "test1");

            //Act
            var mlpFromFullPath = SavingAndRetreiving.GetModelFromJsonFile(pathToFile);

            //Assert
            var output2 = mlpFromFullPath.FeedForward(input1);
            Assert.That(output2.First().Data, Is.EqualTo(output.First().Data).Within(0.0001));
        }
    }
}

