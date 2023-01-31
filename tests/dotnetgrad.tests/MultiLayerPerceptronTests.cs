using System;
namespace dotnetgrad.tests
{
	public class MultiLayerPerceptronTests
	{
		public MultiLayerPerceptronTests()
		{
		}

		[Test]
		public void MultiLayerPerceptronTests_TrainBatch_ConvergesForSimpleDataset()
		{
			//Arrange
			var target1 = -0.25;
			var input1 = new List<double>() { 0.1, 0.8, -0.7 };
            var target2 = 0.0;
            var input2 = new List<double>() { - 0.1, 1.0, 0.1 };
            var target3 = 0.25;
            var input3 = new List<double>() { -0.7, 0.1, 1.0 };
            var mlp = new MultiLayerPerceptron(3, 3, 5, 2);
			var batch1 = new List<DataPoint>() {
				new DataPoint(input1, new List<double>() { target1, target3 }),
				new DataPoint(input2, new List<double>() { target2, target2 })
			};
            var batch2 = new List<DataPoint>() {
                new DataPoint(input1, new List<double>() { target1, target3 }),
                new DataPoint(input3, new List<double>() { target3, target2 })
            };
            var batch3 = new List<DataPoint>() {
                new DataPoint(input3, new List<double>() { target3, target2 }),
                new DataPoint(input2, new List<double>() { target2, target2 })
            };

            //Act
            for (int i = 0; i < 500; i++)
            {
                mlp.TrainBatch(batch2);
                mlp.TrainBatch(batch1);
                mlp.TrainBatch(batch3);
            }

            //Assert
            var outputValues1 = mlp.FeedForward(input1);
            var outputValues2 = mlp.FeedForward(input2);
            var outputValues3 = mlp.FeedForward(input3);
            Assert.That(outputValues1[0].Data, Is.EqualTo(target1).Within(0.01));
            Assert.That(outputValues1[1].Data, Is.EqualTo(target3).Within(0.01));
            Assert.That(outputValues2[0].Data, Is.EqualTo(target2).Within(0.01));
            Assert.That(outputValues2[1].Data, Is.EqualTo(target2).Within(0.01));
            Assert.That(outputValues3[0].Data, Is.EqualTo(target3).Within(0.01));
            Assert.That(outputValues3[1].Data, Is.EqualTo(target2).Within(0.01));
        }
	}
}

