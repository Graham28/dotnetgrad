using System;
using System.Linq.Expressions;

namespace dotnetgrad
{
	public class Neuron
	{
        private readonly Random _random;
        public Value Bias { get; private set; }
		public List<Value> Weights { get; private set; }
		public int NumberOfInputValues { get; init; }
		private Activation _activation;

		public Neuron(int numInputValues, Activation activation = Activation.TanH)
		{
			_random = new Random();
			_activation = activation;
			Bias = new Value(2*(_random.NextDouble() - 0.5));//[-1,1]
			Weights = new List<Value>();
            NumberOfInputValues = numInputValues;
			InitialiseWeights();
		}

		public Value ActivateNeuron(List<Value> inputValues)
		{
			var outputValue = new Value(0.0);
			var i = 0;
			foreach(var weight in Weights)
			{
				outputValue = outputValue.Add(weight.Multiply(inputValues[i]));
				i++;
            }
			outputValue = outputValue.Add(Bias);
            switch (_activation)
            {
                case Activation.TanH:
                    outputValue = outputValue.TanH();
                    break;
				case Activation.Linear:
                default:
                    //Do nothing 
                    break;
            }
            
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

