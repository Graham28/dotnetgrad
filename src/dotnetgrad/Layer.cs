using System;
namespace dotnetgrad
{
	public class Layer
	{
		public List<Neuron> Neurons { get; private set; }

		public Layer(int numInputs, int numOutputs)
		{
			Neurons = new List<Neuron>();
			for (int i = 0; i < numOutputs; i++)
				Neurons.Add(new Neuron(numInputs));
		}

		public List<Value> ActivateLayer(List<Value> inputValues)
		{
			var outputValues = new List<Value>();
			foreach (var neuron in Neurons)
				outputValues.Add(neuron.ActivateNeuron(inputValues));
			return outputValues;
		}

		public List<Value> GetParameters()
		{
			var parameters = new List<Value>();
			foreach (var neuron in Neurons)
				parameters.AddRange(neuron.GetParameters());
			return parameters;
		}
	}
}

