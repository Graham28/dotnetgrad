using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dotnetgrad
{
    public class Layer : ILayer
    {
        public List<Neuron> Neurons { get; set; }
        public Activation Activation { get; set; }

        public Layer(int numInputs, int numOutputs, Activation activation = Activation.TanH)
        {
            Activation = activation;
            Neurons = new List<Neuron>();
            for (int i = 0; i < numOutputs; i++)
                Neurons.Add(new Neuron(numInputs, Activation));
        }
        public Layer()
        { }

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

