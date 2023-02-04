using System;
namespace dotnetgrad.tests
{
	public class SoftmaxLayerTests
	{
		public SoftmaxLayerTests()
		{
		}

		[Test]

		public void SoftmaxLayerTests_ActivateLayer_TotalEqualsOne()
		{
			//Arrange
			var valueList = new List<Value>()
			{
				new Value(6.0),
				new Value(-7.1),
				new Value(1.1)
			};
			var sLayer = new SoftmaxLayer();

			//Act
			var outpuValues = sLayer.ActivateLayer(valueList);

			//Assert
			var total = 0.0;
			foreach (var val in outpuValues)
				total += val.Data;
			Assert.That(total, Is.EqualTo(1.0).Within(0.01));
		}
	}
}

