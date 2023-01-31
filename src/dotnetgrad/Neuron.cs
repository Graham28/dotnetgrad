using System;
namespace dotnetgrad
{
	public class Neuron
	{
        private readonly Random _random;
        public Value Bias { get; private set; }
		public List<Value> Weights { get; private set; }
		public int NumberOfInputValues { get; init; }

		public Neuron(int numInputValues)
		{
			_random = new Random();
			Bias = new Value(2*(_random.NextDouble() - 0.5));//[-1,1]
			Weights = new List<Value>();
            NumberOfInputValues = numInputValues;
			InitialiseWeights();
		}

		public Value ActivateNeuron(List<Value> inputValues)
		{
			var sum = new Value(0.0);
			var i = 0;
			foreach(var weight in Weights)
			{
				sum = sum.Add(weight.Multiply(inputValues[i]));
				i++;
			}
			sum = sum.Add(Bias);
			var outputValue = sum.TanH();
			return outputValue;
		}
		 
		private void InitialiseWeights()
		{
            for (int i = 0; i < NumberOfInputValues; i++)
                Weights.Add(new Value(2 * (_random.NextDouble() - 0.5)));//[-1,1]
        }

		public List<Value> GetParameters()
		{
			var parameters = new List<Value>() { Bias };
			foreach (var weight in Weights)
				parameters.Add(weight);
			return parameters;
		}
	}
}

