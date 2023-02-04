using System;

namespace dotnetgrad
{
	public class SoftmaxLayer : ILayer
	{
        public List<Neuron> Neurons => new List<Neuron>();

        public List<Value> ActivateLayer(List<Value> inputValues)
        {
            var expList = new List<Value>();
            var sumOfExp = new Value(0.0);
            foreach(Value value in inputValues)
            {
                var expOfValue = value.Exp();
                sumOfExp = sumOfExp.Add(expOfValue);
                expList.Add(expOfValue);
            }
            var outputList = new List<Value>();
            foreach (Value value in expList)
            {
                outputList.Add(value.DevideBy(sumOfExp));
            }
            return outputList;
        }

        public List<Value> GetParameters()
        {
            return new List<Value>();
        }
    }
}

