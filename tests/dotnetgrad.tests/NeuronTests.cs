using System;
namespace dotnetgrad.tests
{
	public class NeuronTests
	{
		[Test]
		public void InitialiseActivateAndBackPropNeuronLol()
		{
			var neuron = new Neuron(3);
			var inputs = new List<Value>()
			{
				new Value(0.1),
				new Value(-0.5),
				new Value(0.7)
			};
			var outputValue = neuron.ActivateNeuron(inputs);
			outputValue.Gradient = 1.0;
			outputValue.Backword();

			Assert.NotZero(neuron.Weights[0].Gradient);
            Assert.NotZero(neuron.Weights[1].Gradient);
            Assert.NotZero(neuron.Weights[2].Gradient);
            Assert.NotZero(neuron.Bias.Gradient);
        }
    }
}

