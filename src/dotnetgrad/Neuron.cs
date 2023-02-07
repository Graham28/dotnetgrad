using System;
using System.Linq.Expressions;
using dotnetgrad.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dotnetgrad
{
	public class Neuron
	{
        public Value Bias { get; set; }
		public List<Value> Weights { get; set; }
		public int NumberOfInputValues { get; set; }
        public Activation Activation;

		public Neuron(int numInputValues, Activation activation = Activation.TanH)
		{
			Activation = activation;
			Bias = new Value(0.0);
			Weights = new List<Value>();
            NumberOfInputValues = numInputValues;
			InitialiseWeights();
		}
		//For deserialisation
        public Neuron()
		{ }

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
            switch (Activation)
            {
                case Activation.TanH:
                    outputValue = outputValue.TanH();
                    break;
                case Activation.ReLU:
                    outputValue = outputValue.ReLU();
                    break;
                case Activation.lReLU:
                    outputValue = outputValue.lReLU();
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
                Weights.Add(new Value(MathsHelpers.GetRandomDoubleWithNormalDistribution(0,1)));
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

